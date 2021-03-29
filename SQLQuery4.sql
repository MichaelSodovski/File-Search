/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [id]
      ,[SearchText]
      ,[Dir]
      ,[Result]
  FROM [SystemSearch].[dbo].[Search_tbl]