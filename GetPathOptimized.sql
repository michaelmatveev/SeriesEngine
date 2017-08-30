DECLARE @root int;
SET @root = NULL;

WITH
NetworkNodes (Id, NetId, ParentId, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id, ValidFrom, ValidTill, Tag)
AS(
	SELECT Id, NetId, ParentId, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id, ValidFrom, ValidTill, Tag
	FROM msk1.MainHierarchyNodes AS NN
	WHERE NN.NetId=31
),
NodesWithNames(Id, NetId, ParentId, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id, ValidFrom, ValidTill, Tag, ObjName)
AS
(
	SELECT H.Id, H.NetId, H.ParentId, H.Region_Id, H.Consumer_Id, H.Contract_Id, H.ConsumerObject_Id, H.Point_Id, H.ElectricMeter_Id, H.ValidFrom, H.ValidTill, H.Tag, O1.Name
	FROM NetworkNodes AS H
	INNER JOIN msk1.Regions AS O1
	ON H.Region_Id = O1.Id
	UNION ALL
	SELECT H.Id, H.NetId, H.ParentId, H.Region_Id, H.Consumer_Id, H.Contract_Id, H.ConsumerObject_Id, H.Point_Id, H.ElectricMeter_Id, H.ValidFrom, H.ValidTill, H.Tag, O2.Name
	FROM NetworkNodes AS H
	INNER JOIN msk1.Consumers AS O2
	ON H.Consumer_Id = O2.Id	
	UNION ALL
	SELECT H.Id, H.NetId, H.ParentId, H.Region_Id, H.Consumer_Id, H.Contract_Id, H.ConsumerObject_Id, H.Point_Id, H.ElectricMeter_Id, H.ValidFrom, H.ValidTill, H.Tag, O3.Name
	FROM NetworkNodes AS H
	INNER JOIN msk1.Contracts AS O3
	ON H.Contract_Id = O3.Id
	UNION ALL
	SELECT H.Id, H.NetId, H.ParentId, H.Region_Id, H.Consumer_Id, H.Contract_Id, H.ConsumerObject_Id, H.Point_Id, H.ElectricMeter_Id, H.ValidFrom, H.ValidTill, H.Tag, O4.Name
	FROM NetworkNodes AS H
	INNER JOIN msk1.ConsumerObjects AS O4
	ON H.ConsumerObject_Id = O4.Id
	UNION ALL
	SELECT H.Id, H.NetId, H.ParentId, H.Region_Id, H.Consumer_Id, H.Contract_Id, H.ConsumerObject_Id, H.Point_Id, H.ElectricMeter_Id, H.ValidFrom, H.ValidTill, H.Tag, O5.Name
	FROM NetworkNodes AS H
	INNER JOIN msk1.Points AS O5
	ON H.Point_Id = O5.Id
	UNION ALL
	SELECT H.Id, H.NetId, H.ParentId, H.Region_Id, H.Consumer_Id, H.Contract_Id, H.ConsumerObject_Id, H.Point_Id, H.ElectricMeter_Id, H.ValidFrom, H.ValidTill, H.Tag, O6.Name
	FROM NetworkNodes AS H
	INNER JOIN msk1.ElectricMeters AS O6
	ON H.ElectricMeter_Id = O6.Id
), 
Paths(Id, NetId, ParentId, Name, NamePath, Level, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id, ValidFrom, ValidTill, Tag)
AS
(
	SELECT 
		H.Id,
		H.NetId, 
		H.ParentId,
		ObjName AS Name, 
		ObjName AS NamePath, 
		0 AS Level, 
		Region_Id, 
		Consumer_Id, 
		Contract_Id, 
		ConsumerObject_Id, 
		Point_Id, 
		ElectricMeter_Id, 
		ValidFrom, 
		ValidTill, 
		Tag FROM NodesWithNames AS H
	--WHERE H.ParentId IS NULL
	WHERE H.ParentId = @root OR (@root IS NULL AND H.ParentId IS NULL)
	UNION ALL
	SELECT 
		H.Id,
		H.NetId, 
		H.ParentId, 
		ObjName AS Name, 
		R.NamePath + '|' + H.ObjName AS NamePath, 
		R.Level + 1, 
		H.Region_Id, 
		H.Consumer_Id, 
		H.Contract_Id, 
		H.ConsumerObject_Id, 
		H.Point_Id, 
		H.ElectricMeter_Id, 
		H.ValidFrom, 
		H.ValidTill, 
		H.Tag FROM NodesWithNames AS H
	INNER JOIN Paths AS R
	ON H.ParentId = R.Id
)
SELECT * FROM Paths

----SELECT COUNT(*) FROM msk1.MainHierarchyNodes2
----SELECT * FROM msk1.MainHierarchyView WHERE NetId=31 And NamePath LIKE 'Пензенская область|ООО "МагнитЭнерго"|1001014-ЭН%';

--WITH NodesWithNames(Id, NetId, ParentId, ObjName, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id, ValidFrom, ValidTill, Tag)
--AS
--(
--	SELECT 
--		H.Id,
--		H.NetId,
--		H.ParentId,
--		COALESCE(O1.Name, O2.Name, O3.Name, O4.Name, O5.Name, O6.Name) AS ObjName,
--		Region_Id,
--		Consumer_Id,
--		Contract_Id,
--		ConsumerObject_Id,
--		Point_Id,
--		ElectricMeter_Id,
--		ValidFrom,
--		ValidTill,
--		H.Tag
--	FROM msk1.MainHierarchyNodes2 AS H
--	INNER JOIN msk1.Regions AS O1
--	ON H.Region_Id = O1.Id
--	INNER JOIN msk1.Consumers AS O2
--	ON H.Consumer_Id = O2.Id
--	INNER JOIN msk1.Contracts AS O3
--	ON H.Contract_Id = O3.Id
--	INNER JOIN msk1.ConsumerObjects AS O4
--	ON H.ConsumerObject_Id = O4.Id
--	INNER JOIN msk1.Points AS O5
--	ON H.Point_Id = O5.Id
--	INNER JOIN msk1.ElectricMeters AS O6
--	ON H.ElectricMeter_Id = O6.Id
--), 
--Paths(Id, NetId, ParentId, Name, NamePath, Level, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id, ValidFrom, ValidTill, Tag)
--AS
--(
--	SELECT 
--		H.Id,
--		H.NetId, 
--		H.ParentId,
--		ObjName AS Name, 
--		ObjName AS NamePath, 
--		0 AS Level, 
--		Region_Id, 
--		Consumer_Id, 
--		Contract_Id, 
--		ConsumerObject_Id, 
--		Point_Id, 
--		ElectricMeter_Id, 
--		ValidFrom, 
--		ValidTill, 
--		Tag FROM NodesWithNames AS H
--	WHERE H.ParentId IS NULL
--	UNION ALL
--	SELECT 
--		H.Id,
--		H.NetId, 
--		H.ParentId, 
--		ObjName AS Name, 
--		R.NamePath + '|' + H.ObjName AS NamePath, 
--		R.Level + 1, 
--		H.Region_Id, 
--		H.Consumer_Id, 
--		H.Contract_Id, 
--		H.ConsumerObject_Id, 
--		H.Point_Id, 
--		H.ElectricMeter_Id, 
--		H.ValidFrom, 
--		H.ValidTill, 
--		H.Tag FROM NodesWithNames AS H
--	INNER JOIN Paths AS R
--	ON H.ParentId = R.Id
--)
--SELECT * FROM Paths