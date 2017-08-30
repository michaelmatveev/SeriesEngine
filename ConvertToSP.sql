set statistics io on

;WITH NetworkNodes (Id, ParentId, ValidFrom, ValidTill, Tag, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id) AS
(
	SELECT Id, ParentId, ValidFrom, ValidTill, Tag, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id
	FROM msk1.MainHierarchyNodes
	WHERE NetId=31
),
NamedNodes(Id, ParentId, ValidFrom, ValidTill, Tag, ObjName, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id) AS
(
	SELECT NN.Id, NN.ParentId, NN.ValidFrom, NN.ValidTill, NN.Tag, O.Name, NN.Region_Id, NULL, NULL, NULL, NULL, NULL
	FROM NetworkNodes AS NN
	INNER JOIN msk1.Regions AS O
	ON NN.Region_Id = O.Id
	UNION ALL
	SELECT NN.Id, NN.ParentId, NN.ValidFrom, NN.ValidTill, NN.Tag, O.Name, NULL, Consumer_Id, NULL, NULL, NULL, NULL 
	FROM NetworkNodes AS NN
	INNER JOIN msk1.Consumers AS O
	ON NN.Consumer_Id = O.Id
	UNION ALL
	SELECT NN.Id, NN.ParentId, NN.ValidFrom, NN.ValidTill, NN.Tag, O.Name, NULL, NULL, Contract_Id, NULL, NULL, NULL 
	FROM NetworkNodes AS NN
	INNER JOIN msk1.Contracts AS O
	ON NN.Contract_Id = O.Id
	UNION ALL
	SELECT NN.Id, NN.ParentId, NN.ValidFrom, NN.ValidTill, NN.Tag, O.Name, NULL, NULL, NULL, ConsumerObject_Id, NULL, NULL 
	FROM NetworkNodes AS NN
	INNER JOIN msk1.ConsumerObjects AS O
	ON NN.Consumer_Id = O.Id
	UNION ALL
	SELECT NN.Id, NN.ParentId, NN.ValidFrom, NN.ValidTill, NN.Tag, O.Name, NULL, NULL, NULL, NULL, Point_Id, NULL 
	FROM NetworkNodes AS NN
	INNER JOIN msk1.Points AS O
	ON NN.Point_Id = O.Id
	UNION ALL
	SELECT NN.Id, NN.ParentId, NN.ValidFrom, NN.ValidTill, NN.Tag, O.Name, NULL, NULL, NULL, NULL, NULL, ElectricMeter_Id 
	FROM NetworkNodes AS NN
	INNER JOIN msk1.ElectricMeters AS O
	ON NN.ElectricMeter_Id = O.Id
),
Paths(Id, ParentId, Name, NamePath, Level, ValidFrom, ValidTill, Tag, Region_Id, Consumer_Id, Contract_Id, ConsumerObject_Id, Point_Id, ElectricMeter_Id)
AS
(
	SELECT 
		H.Id,
		H.ParentId,
		ObjName AS Name, 
		ObjName AS NamePath, 
		0 AS Level, 
		ValidFrom, 
		ValidTill,
		Tag, 
		Region_Id, 
		Consumer_Id, 
		Contract_Id, 
		ConsumerObject_Id, 
		Point_Id, 
		ElectricMeter_Id
		FROM NodesWithNames AS H
	WHERE H.ParentId IS NULL
--	WHERE H.ParentId = @root OR (@root IS NULL AND H.ParentId IS NULL)
	UNION ALL
	SELECT 
		H.Id,
		H.ParentId, 
		ObjName AS Name, 
		R.NamePath + '|' + H.ObjName AS NamePath, 
		R.Level + 1, 
		H.ValidFrom, 
		H.ValidTill, 
		H.Tag,
		H.Region_Id, 
		H.Consumer_Id, 
		H.Contract_Id, 
		H.ConsumerObject_Id, 
		H.Point_Id, 
		H.ElectricMeter_Id
	FROM NodesWithNames AS H
	INNER JOIN Paths AS R
	ON H.ParentId = R.Id
)
SELECT * FROM Paths


