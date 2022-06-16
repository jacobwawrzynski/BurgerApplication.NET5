use master
go
drop database if exists [BurgerAppDataBase]
go
create database [BurgerAppDataBase]
go
use [BurgerAppDataBase]
go
drop table if exists [Products]
go
create table [Products](
[Id] int identity(1,1) primary key not null,
[Name] varchar(MAX) not null,
[Price] smallmoney not null,
[Description] varchar(MAX) null
)

drop table if exists [Allergens]
go
create table [Allergens](
[Id] int identity(1,1) primary key not null,
[Name] varchar(MAX) not null
)
go
drop table if exists [Products_Allergens]
go
create table [Products_Allergens](
[Id] int identity(1,1) primary key not null,
[Id_Product] int not null, 
[Id_Allergen] int not null, 
constraint [Prod_Prod_Alle] foreign key (Id_Product) references [Products](Id),
constraint [Alle_Prod_Alle] foreign key (Id_Allergen) references [Allergens](Id)
)
go 
drop table if exists [Discount_Codes]
go
create table [Discount_Codes](
[Id] int identity(1,1) primary key not null,
[Code] varchar(50) not null unique,
[Percent] int not null check([Percent] >= 1 and [Percent] <=50),
[Minimum_Order_Amount] int not null check([Minimum_Order_Amount]  >= 0 ),
[Quantity] int check([Quantity] >= 0 ) null
)
go
drop table if exists [Addresses]
go
create table [Addresses](
[Id] int identity(1,1) primary key not null,
[City] varchar(MAX) not null,
[Zip_Code] char(6) NOT NULL CHECK([Zip_Code] like '[0-9][0-9][-][0-9][0-9][0-9]'),
[Street] nvarchar(40) null,
[House_Number] varchar(5) NOT NULL,
[Apartment_Number] varchar(3) null
)
go
drop table if exists [Restaurants]
go
create table [Restaurants](
[Id] int identity(1,1) primary key not null,
[Id_Address] int not null unique,
constraint [Addr_Rest] foreign key (Id_Address) references [Addresses](Id)
)
go
drop table if exists [Customers]
go
create table [Customers](
[Id] int identity(1,1) primary key not null,
[Email] nvarchar(320) not null unique CHECK([Email] LIKE '%[@]%[.]%'),
[Code] varchar(8) not null unique,
)
go
drop table if exists [Staff]
go
create table [Staff](
[Id] int identity(1,1) primary key not null,
[Login] varchar(40) not null unique,
[Password] varchar(MAX) not null,
[Role] varchar(50) not null CHECK([Role] in ('Employee','Manager','Owner')),
[Name] varchar(50) not null,
[Last_Name] varchar(50) not null,
[Pesel] char(11) not null unique CHECK([Pesel] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
[Email] nvarchar(320) unique CHECK([Email] LIKE '%[@]%[.]%'),
[Creation_Date] datetime not null default GETDATE(),
[Deletion_Date] datetime null,
[Id_Address] int not null unique,
[Id_Restaurant] int not null,
constraint [Addr_Staf] foreign key (Id_Address) references [Addresses](Id),
constraint [Rest_Staf] foreign key (Id_Restaurant) references [Restaurants](Id)
)
go
drop table if exists [Orders]
go
create table [Orders](
[Id] int identity(1,1) primary key not null,
[Id_Staff] int not null,
[Id_Customer] int null,
[Id_Discount_Code] int null,
[Date] datetime not null default GETDATE()
constraint [Staf_Order] foreign key (Id_Staff) references [Staff](Id),
constraint [Cust_Order] foreign key (Id_Customer) references [Customers](Id),
constraint [Disc_Code_Order] foreign key (Id_Discount_Code) references [Discount_Codes](Id)
)
go
drop table if exists [Products_Orders]
go
create table [Products_Orders](
[Id] int identity(1,1) primary key not null,
[Id_Product] int not null, 
[Id_Order] int not null, 
constraint [Prod_Prod_Orde] foreign key (Id_Product) references [Products](Id),
constraint [Orde_Prod_Orde] foreign key (Id_Order) references [Orders](Id)
)
drop table if exists [Delivery]
go
create table [Delivery](
[Id] int identity(1,1) primary key not null,
[File] varbinary(max) not null,
[Date] datetime2 not null default GETDATE(),
[Id_Restaurant] int not null,
constraint [Rest_Deli] foreign key (Id_Restaurant) references [Restaurants](Id)
)
go
drop table if exists [Invoices]
go
create table [Invoices](
[Id] int identity(1,1) primary key not null,
[File] varbinary(max) not null,
[Date] datetime2 not null default GETDATE(),
[Id_Restaurant] int not null,
constraint [Rest_Invo] foreign key (Id_Restaurant) references [Restaurants](Id)
)
go
drop table if exists [Reports]
go
create table [Reports](
[Id] int identity(1,1) primary key not null,
[File] varbinary(max) not null,
[Date] datetime2 not null default GETDATE(),
[Id_Restaurant] int not null,
constraint [Rest_Repo] foreign key (Id_Restaurant) references [Restaurants](Id)
)
go
drop table if exists [Images]
go
create table [Images](
[Id] int identity(1,1) primary key not null,
[ImageData] Image not null,
[Alt_Text] nvarchar(40) not null unique

)
go

insert into Products values('Hamburger',20.50,'200g 100% wo³owiny, pomodor, sa³ata, pikle, czerwona cebula oraz w³asnorêcznie robony sos')
insert into Products values('Cheeseburger',22.00,'200g 100% wo³owiny, ser cheedar, ser mimolette, pikle, czerwona cebula oraz w³asnorêcznie robiony sos')
insert into Products values('Chili Burger',22.00,'200g 100% wo³owiny, pomodor, sa³ata, pikle, czerwona cebula, papryka jalapeno, tabasco oraz w³asnorêcznie robony ostry sos')
insert into Products values('Mexican Burger',23.00,'200g 100% wo³owiny, pomodor, sa³ata, pikle, czerwona cebula, papryka jalapeno, nachosy, salsa, sos serowy')
insert into Products values('Double Burger',27.50,'400g 100% wo³owiny, pomodor, sa³ata, pikle, czerwona cebula oraz w³asnorêcznie robony sos')
insert into Products values('DIY Burger',25.50,'200g wo³owiny oraz wolna amerykanka, z³ó¿ swojego burgera!')
insert into Products values('Frytki',7.50,'Du¿a porcja cieñnich frytek z 1 sosem do wybory(ketchup, majonez, serowy)')


insert into Allergens values('Laktoza')
insert into Allergens values('Gluten')
insert into Allergens values('Kapsaicyna')
insert into Allergens values('Sezam')
insert into Products_Allergens values(1,2)
insert into Products_Allergens values(1,4)
insert into Products_Allergens values(2,2)
insert into Products_Allergens values(2,1)
insert into Products_Allergens values(2,4)
insert into Products_Allergens values(3,2)
insert into Products_Allergens values(3,4)
insert into Products_Allergens values(3,3)
insert into Products_Allergens values(4,3)
insert into Products_Allergens values(4,2)
insert into Products_Allergens values(4,4)
insert into Products_Allergens values(5,2)
insert into Products_Allergens values(5,4)
insert into Products_Allergens values(6,2)
insert into Products_Allergens values(6,4)

insert into Addresses values('Kraków','30-999','Warszawska','25b',null)
insert into Addresses values('Kraków','30-696','Filipa','10',null)
insert into Addresses values('Kraków','30-696','Filipa','10',null)
insert into Restaurants values(1)
insert into Staff values('GigaSzef','NieSzanujeKuca1@','Manager','Adam','Nowak','01347412352','lubiekoty@wsei.com',default,null,2,1)
insert into Staff values('owner','NieSzanujeKuca1@','Owner','¯yd','Pawlikowski','92318413451','nielubiekoty@wsei.com',default,null,3,1)
insert into Discount_Codes values('Burger15',15,20,null)
insert into Discount_Codes values('Family30',30,60,null)
insert into Orders values(1,null,1,default)
insert into Products_Orders values(1,1)
insert into Products_Orders values(2,1)
