CREATE PROCEDURE Pagination 
	@Table VARCHAR(100),
	@PageSize INT, 
	@Page INT, 
	@SortDirection VARCHAR(4), 
	@PropertySortDirection VARCHAR(100), 
	@ValueFilter NVARCHAR(MAX)
AS 
BEGIN;
	DECLARE @Sql NVARCHAR(MAX);
	DECLARE @PagesQuantity INT;
	DECLARE @TotalRows INT;
	DECLARE @TotalPages INT;
	DECLARE @Offset INT;
	DECLARE @IsExistPropertySortDirection INT = 0;
	
	----------------------Inicio obtener nombres de las columnas de una tabla----------------------
	DECLARE @ColumnName NVARCHAR(MAX);
	DECLARE @ConcatColumnLikeLower NVARCHAR(MAX);
	DECLARE @ConcatColumns NVARCHAR(MAX);
	DECLARE @Cursor CURSOR;

	-- Obtiene el nombre de columnas una tabla
	SET @Cursor = CURSOR FOR SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Table AND DATA_TYPE != 'int';
	OPEN @Cursor;
	
	FETCH NEXT FROM @Cursor INTO @ColumnName;
    WHILE @@FETCH_STATUS = 0
		BEGIN;
			-- Verifica si la columna que viene en PropertySortDirection existe
			IF @ColumnName = @PropertySortDirection
			BEGIN;
				SET @IsExistPropertySortDirection = 1;
			END;

			SET @ConcatColumns = CONCAT(@ConcatColumns, CONCAT(@ColumnName, ', '));
			SET @ConcatColumnLikeLower = CONCAT(@ConcatColumnLikeLower, CONCAT('LOWER(' + @ColumnName + ') ', 'LIKE LOWER(''%' + @ValueFilter + '%'') OR '));
			FETCH NEXT FROM @Cursor INTO @ColumnName;
		END;
    CLOSE @Cursor;
    DEALLOCATE @Cursor;
	
	-- Se elimina la ultima coma y "OR" de las cadenas de texto
	SET @ConcatColumns = LEFT(@ConcatColumns, LEN(@ConcatColumns) - 1);
	SET @ConcatColumnLikeLower = LEFT(@ConcatColumnLikeLower, LEN(@ConcatColumnLikeLower) - 2);

	--PRINT @ConcatColumns;
	--PRINT @ConcatColumnLikeLower;

	----------------------Final obtener nombres de las columnas de una tabla----------------------

	----------------------    Inicio obtener informacion con paginacion     ----------------------

	-- Obtiene el total de rows que tiene la tabla
	SET @Sql = N'SELECT @Output = COUNT(' + @PropertySortDirection + ') FROM dbo.' + @Table;
	EXEC SP_EXECUTESQL @Sql, N'@Output INT OUTPUT', @Output = @TotalRows OUTPUT;
	
	IF @ValueFilter is NULL OR @ValueFilter = ''
	BEGIN;
		SET @ValueFilter = '';
	END;

	-- Concatena el nombre de la columna que venga PropertySortDirection caso que no exista la ConcatColumns.
	IF @IsExistPropertySortDirection = 0
	BEGIN;
		SET @ConcatColumns = CONCAT(@PropertySortDirection + ', ', @ConcatColumns);
	END;

	SET @Offset = (@Page - 1) * @PageSize;
	SET @Sql = 'SELECT ' + @ConcatColumns + ' 
				FROM dbo.' + @Table + ' 
				WHERE ' + @ConcatColumnLikeLower + '
				ORDER BY ' + @PropertySortDirection + ' ' + @SortDirection + ' 
				OFFSET ' + CONVERT(VARCHAR, @Offset) + ' 
				ROWS FETCH NEXT ' + CONVERT(VARCHAR, @PageSize) + ' 
				ROWS ONLY';

	PRINT @Sql;
	EXEC SP_EXECUTESQL @Sql;
	
	--PRINT @Sql;

	----------------------  Final para obtener informacion con paginacion   ----------------------
END;

EXEC dbo.Pagination @Table = 'Movies',  @PageSize = 20, @Page = 2, @SortDirection = 'ASC', @PropertySortDirection = 'Id', @ValueFilter = '';

DROP PROCEDURE Pagination;