CREATE TABLE Suppliers
(
Id int not null identity primary key,
SupplierName nvarchar(max) not null,
ContactInfo nvarchar(max) not null

)


CREATE TABLE Manufacturers 
(
Id int not null identity primary key,
ProductId int not null,
ManufacturerName nvarchar(max) not null
)


CREATE TABLE ProductAttributes
(
Id int not null identity primary key,
ProductId int not null,
AttributeName nvarchar(max) not null,
)

CREATE TABLE ProductImages
(
Id int not null identity primary key,
ProductId int not null,
ImagePath nvarchar(max) not null

)

CREATE TABLE Reviews 
(
Id int not null identity primary key,
ProductId int not null,
UserId int not null,
Comment nvarchar(max) not null,
)


