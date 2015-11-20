USE [master]
GO

/****** Object:  Database [Sales]    Script Date: 2015-11-20 13:24:16 ******/
CREATE DATABASE [Sales]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Sales', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Sales.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Sales_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Sales_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO