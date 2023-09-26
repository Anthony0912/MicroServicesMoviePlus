CREATE PROCEDURE QuantityItemsInTable
	@TableName VARCHAR(100),
	@PropertySortDirection VARCHAR(100), 
	@ValueFilter NVARCHAR(MAX)
AS 
BEGIN;
	DECLARE @Sql NVARCHAR(MAX);

	----------------------Inicio obtener nombres de las columnas de una tabla----------------------
	DECLARE @ColumnName NVARCHAR(MAX);
	DECLARE @ConcatColumnLikeLower NVARCHAR(MAX);
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
			SET @ConcatColumnLikeLower = CONCAT(@ConcatColumnLikeLower, CONCAT('LOWER(' + @ColumnName + ') ', 'LIKE LOWER(''%' + @ValueFilter + '%'') OR '));
			FETCH NEXT FROM @Cursor INTO @ColumnName;
		END;
    CLOSE @Cursor;
    DEALLOCATE @Cursor;
	
	-- Se elimina la ultima coma y "OR" de las cadenas de texto
	SET @ConcatColumnLikeLower = LEFT(@ConcatColumnLikeLower, LEN(@ConcatColumnLikeLower) - 2);

	-- Obtiene el total de rows que tiene la tabla
	SET @Sql = 'SELECT 
				COUNT(' + @PropertySortDirection + ') AS QuantityItems 
				FROM dbo.' + @TableName + ' 
				WHERE ' + @ConcatColumnLikeLower;
	PRINT @Sql
	EXEC SP_EXECUTESQL @Sql;
END;

EXEC dbo.QuantityItemsInTable @TableName = 'Movies', @PropertySortDirection = 'Id', @ValueFilter = '';

DROP PROCEDURE QuantityItemsInTable;