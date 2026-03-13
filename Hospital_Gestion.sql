CREATE DATABASE Hospital_Turnos_Traige;
GO
USE Hospital_Turnos_Traige;
GO


CREATE TABLE Prioridad (
    id_prioridad  INT         NOT NULL IDENTITY(1,1),
    nombre        VARCHAR(20) NOT NULL,
    nivel         INT         NOT NULL,
    CONSTRAINT pk_prioridad       PRIMARY KEY (id_prioridad),
    CONSTRAINT uq_prioridad_nivel UNIQUE (nivel)
);
GO

CREATE TABLE Paciente (
    id_paciente  INT         NOT NULL IDENTITY(1,1),
    dni          VARCHAR(15) NOT NULL,
    nombre       VARCHAR(60) NOT NULL,
    telefono     VARCHAR(20) NOT NULL,
    CONSTRAINT pk_paciente     PRIMARY KEY (id_paciente),
    CONSTRAINT uq_paciente_dni UNIQUE (dni)
);
GO

CREATE TABLE Turno (
    id_turno    INT          NOT NULL IDENTITY(1,1),
    id_paciente INT          NOT NULL,
    id_prioridad INT         NOT NULL,
    nro_turno   VARCHAR(10)  NOT NULL,
    motivo      VARCHAR(150) NOT NULL,
    fecha_ingreso DATETIME   NOT NULL DEFAULT GETDATE(),
    fecha_atencion DATETIME,
    estado      VARCHAR(15)  NOT NULL DEFAULT 'En espera',
    CONSTRAINT pk_turno       PRIMARY KEY (id_turno),
    CONSTRAINT fk_turno_pac   FOREIGN KEY (id_paciente)  REFERENCES Paciente(id_paciente),
    CONSTRAINT fk_turno_prio  FOREIGN KEY (id_prioridad) REFERENCES Prioridad(id_prioridad),
    CONSTRAINT chk_estado     CHECK (estado IN ('En espera','Atendido','Cancelado'))
);
GO

CREATE TABLE Consulta (
    id_consulta INT          NOT NULL IDENTITY(1,1),
    id_turno    INT          NOT NULL,
    medico      VARCHAR(80)  NOT NULL,
    diagnostico VARCHAR(200) NOT NULL,
    hora_inicio DATETIME     NOT NULL DEFAULT GETDATE(),
    hora_fin    DATETIME,
    CONSTRAINT pk_consulta       PRIMARY KEY (id_consulta),
    CONSTRAINT uq_consulta_turno UNIQUE (id_turno),
    CONSTRAINT fk_consulta_turno FOREIGN KEY (id_turno) REFERENCES Turno(id_turno)
);
GO



INSERT INTO Prioridad (nombre, nivel) VALUES
('Emergencia',   1),
('Urgente',      2),
('Semi-urgente', 3),
('No urgente',   4);

INSERT INTO Paciente (dni, nombre, telefono) VALUES
('301', 'Luis García',    '0991234567'),
('222', 'Ana Rodríguez',  '0997654321'),
('355', 'Carlos Martínez','0993456789');

INSERT INTO Turno (id_paciente, id_prioridad, nro_turno, motivo, fecha_ingreso, fecha_atencion, estado) VALUES
(1, 1, 'T-0001', 'Dolor en el pecho',         '2026-02-10 08:00:00', '2026-02-10 08:15:00', 'Atendido'),
(2, 3, 'T-0002', 'Fiebre leve',               '2026-02-10 08:10:00', '2026-02-10 09:00:00', 'Atendido'),
(3, 2, 'T-0003', 'Fractura de muñeca',        '2026-02-10 08:20:00', NULL,                  'En espera');

INSERT INTO Consulta (id_turno, medico, diagnostico, hora_inicio, hora_fin) VALUES
(1, 'Dr.Baez',  'Angina inestable',  '2026-02-10 08:15:00', '2026-02-10 08:45:00'),
(2, 'Dra. Carvajal', 'Faringitis aguda',  '2026-02-10 09:00:00', '2026-02-10 09:20:00');
GO


SELECT
    T.nro_turno,
    PR.nivel,
    PR.nombre                                          AS prioridad,
    P.nombre                                           AS paciente,
    T.motivo,
    T.fecha_ingreso,
    DATEDIFF(MINUTE, T.fecha_ingreso, GETDATE())       AS minutos_espera
FROM Turno T
JOIN Prioridad PR ON T.id_prioridad = PR.id_prioridad
JOIN Paciente  P  ON T.id_paciente  = P.id_paciente
WHERE T.estado = 'En espera'
ORDER BY PR.nivel ASC, T.fecha_ingreso ASC;
GO


SELECT
    PR.nivel,
    PR.nombre                                                           AS prioridad,
    COUNT(T.id_turno)                                                   AS total_atendidos,
    ROUND(AVG(CAST(DATEDIFF(MINUTE,
          T.fecha_ingreso, T.fecha_atencion) AS FLOAT)), 1)             AS espera_promedio_min
FROM Turno T
JOIN Prioridad PR ON T.id_prioridad = PR.id_prioridad
WHERE T.estado = 'Atendido'
GROUP BY PR.id_prioridad, PR.nivel, PR.nombre
ORDER BY PR.nivel;
GO