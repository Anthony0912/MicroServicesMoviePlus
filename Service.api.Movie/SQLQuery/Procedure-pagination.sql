CREATE PROCEDURE Pagination 
	@TableName VARCHAR(100),
	@PageSize INT, 
	@Page INT, 
	@SortDirection VARCHAR(4), 
	@PropertySortDirection VARCHAR(100), 
	@ValueFilter NVARCHAR(MAX)
AS 
BEGIN;
	DECLARE @Sql NVARCHAR(MAX);
	DECLARE @Offset INT;

	----------------------Inicio obtener nombres de las columnas de una tabla----------------------
	DECLARE @ColumnName NVARCHAR(MAX);
	DECLARE @ConcatColumnLikeLower NVARCHAR(MAX);
	DECLARE @ConcatColumns NVARCHAR(MAX);
	DECLARE @Cursor CURSOR;

	-- Valida si valor a filtrar es null
	IF @ValueFilter is NULL
	BEGIN;
		SET @ValueFilter = '';
	END;

	-- Obtiene el nombre de columnas una tabla
	SET @Cursor = CURSOR FOR SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName;
	OPEN @Cursor;
	
	FETCH NEXT FROM @Cursor INTO @ColumnName;
    WHILE @@FETCH_STATUS = 0
		BEGIN;
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
	
	SET @Offset = (@Page - 1) * @PageSize;
	SET @Sql = 'SELECT ' + @ConcatColumns + ' 
				FROM dbo.' + @TableName + ' 
				WHERE ' + @ConcatColumnLikeLower + '
				ORDER BY ' + @PropertySortDirection + ' ' + @SortDirection + ' 
				OFFSET ' + CONVERT(VARCHAR, @Offset) + ' 
				ROWS FETCH NEXT ' + CONVERT(VARCHAR, @PageSize) + ' 
				ROWS ONLY';

	-- PRINT @Sql;
	EXEC SP_EXECUTESQL @Sql;
	
	-- PRINT @Sql;

	----------------------  Final para obtener informacion con paginacion   ----------------------
END;

EXEC dbo.Pagination @TableName = 'Movies',  @PageSize = 20, @Page = 1, @SortDirection = 'ASC', @PropertySortDirection = 'Id', @ValueFilter = 'costa';

DROP PROCEDURE Pagination;