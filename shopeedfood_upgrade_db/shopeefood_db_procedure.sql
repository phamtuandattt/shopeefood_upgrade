-- Get business fields in the city
CREATE PROC GET_BUSINESS_IN_THE_CITY @CITYID INT
AS
BEGIN
	SELECT Cities.CityID, BusinessFields.FieldName
	FROM Cities, BusinessFields, CityBusinessFields
	WHERE Cities.CityID = CityBusinessFields.CityID 
		AND BusinessFields.FieldID = CityBusinessFields.FieldID
		AND Cities.CityID = @CITYID
END
EXEC GET_BUSINESS_IN_THE_CITY 2

-- Get shop in the city
GO
CREATE PROC GET_SHOP_BUSINESS_IN_THE_CITY @CITYID INT, @FIELDID INT, @PageNumber INT, @PageSize INT
AS
BEGIN
	SET NOCOUNT ON;
	WITH PaginatedShops AS 
    (
		SELECT CityBusinessFieldsShop.CityID AS CityID, CityBusinessFieldsShop.FieldID AS FieldID,
				CityBusinessFieldsShop.ShopID AS ShopID, Shops.ShopName AS ShopName,
				Shops.ShopImage AS ShopImage, Shops.ShopAddress AS ShopAddress, Shops.ShopUptime AS ShopUptime,
				COUNT(*) OVER() AS TotalRecords,  -- Get total count for pagination
				ROW_NUMBER() OVER (ORDER BY ShopName ASC) AS RowNum  -- Pagination ordering
		FROM CityBusinessFieldsShop, Shops
		WHERE CityBusinessFieldsShop.ShopID = Shops.ShopID
			AND CityBusinessFieldsShop.CityID = @CITYID
			AND CityBusinessFieldsShop.FieldID = @FIELDID
    )
	
	SELECT CityID, FieldID, ShopID, ShopName, ShopImage, ShopAddress, ShopUptime, TotalRecords
    FROM PaginatedShops
    WHERE RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;
END
EXEC GET_SHOP_BUSINESS_IN_THE_CITY 1, 1, 1, 6


