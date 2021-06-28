Create database bdtrailer

use bdtrailer

Create table usuarios (
id int identity primary key,
nombre varchar(50),
usuario varchar(50) unique,
clave varchar(50),
)

Create table trailers (
id int identity primary key,
nombre varchar(50),
link varchar(300),
descripcion varchar(250),
)

select * from usuarios
select * from trailers

truncate table trailers