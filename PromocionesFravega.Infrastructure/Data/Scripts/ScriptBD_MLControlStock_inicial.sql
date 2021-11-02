IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MLControlStock')
  BEGIN
    CREATE DATABASE MLControlStock
  END

USE [MLControlStock]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 19/10/2021 20:48:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[deposito] [varchar](4) NOT NULL,
	[area] [varchar](2) NOT NULL,
	[pasillo] [varchar](2) NOT NULL,
	[fila] [varchar](2) NOT NULL,
	[cara] [varchar](2) NOT NULL,
	[product_id] [varchar](50) NOT NULL,
	[cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[deposito] ASC,
	[area] ASC,
	[pasillo] ASC,
	[fila] ASC,
	[cara] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'AL', N'01', N'05', N'DE', N'MLA864423456', 7)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'AL', N'39', N'78', N'DE', N'MLA813727183', 22)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'AL', N'39', N'78', N'IZ', N'MLA813727183', 87)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'LM', N'00', N'00', N'IZ', N'MLA813727183', 50)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'LM', N'00', N'00', N'IZ', N'MLA834851363', 8)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'LM', N'00', N'00', N'IZ', N'MLA903598301', 8)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'LM', N'08', N'75', N'DE', N'MLA864423456', 35)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR01', N'LM', N'20', N'12', N'DE', N'MLA875689035', 99)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR02', N'AL', N'01', N'05', N'IZ', N'MLA864423456', 7)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'AR02', N'AL', N'77', N'77', N'DE', N'MLA864423456', 44)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'BR05', N'AL', N'77', N'77', N'DE', N'MLA827561145', 77)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'BR05', N'AL', N'99', N'99', N'DE', N'MLA907245254', 12)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'BR05', N'AL', N'99', N'99', N'DE', N'MLA918518849', 48)
GO
INSERT [dbo].[Stock] ([deposito], [area], [pasillo], [fila], [cara], [product_id], [cantidad]) VALUES (N'BR05', N'LM', N'00', N'00', N'IZ', N'MLA918518849', 5)
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerStockPorProducto]    Script Date: 19/10/2021 20:48:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_ObtenerStockPorProducto](@deposito varchar(4)  ,@product_id varchar(50))
AS
BEGIN

	SELECT deposito,
		   area + '-' + pasillo + '-' + fila +'-'+ cara ubicacion,
		   cantidad 
	FROM stock
	WHERE deposito = @deposito AND product_id = @product_id


END
GO
