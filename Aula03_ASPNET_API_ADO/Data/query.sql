CREATE TABLE Maquina (
 Id_Maquina INT PRIMARY KEY NOT NULL,
 Tipo VARCHAR(255),
 Velocidade INT,
 HardDisk INT,
 Placa_Rede INT,
 Memoria_Ram INT,
 Fk_Usuario INT,
 FOREIGN KEY(Fk_Usuario) REFERENCES Usuarios(ID_Usuario)
);
CREATE TABLE Usuarios (
 ID_Usuario INT PRIMARY KEY NOT NULL,
 Password VARCHAR(255),
 Nome_Usuario VARCHAR(255),
 Ramal INT,
 Especialidade VARCHAR(255)
);
CREATE TABLE Software (
 Id_Software INT PRIMARY KEY NOT NULL,
 Produto VARCHAR(255),
 HardDisk INT,
 Memoria_Ram INT,
 Fk_Maquina INT,
 FOREIGN KEY(Fk_Maquina) REFERENCES Maquina(Id_Maquina)
);

insert into Maquina values (1, 'Desktop', 2, 500, 1, 4, 1);
insert into Maquina values (2, 'Notebook', 1, 250, 1, 2, 2);
insert into Maquina values (3, 'Desktop', 3, 1000, 1, 8, 3);
insert into Maquina values (4, 'Notebook', 2, 500, 1, 4, 4);
insert into Usuarios values (1, '123', 'Joao', 123, 'TI');
insert into Usuarios values (2, '456', 'Maria', 456, 'RH');
insert into Usuarios values (3, '789', 'Jose', 789, 'Financeiro');
insert into Usuarios values (4, '101', 'Ana', 101, 'TI');
insert into Software values (1, 'Windows', 100, 2, 1);
insert into Software values (2, 'Linux', 50, 1, 2);
insert into Software values (3, 'Windows', 200, 4, 3);
insert into Software values (4, 'Linux', 100, 2, 4);

SELECT * from usuario;