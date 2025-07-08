BEGIN;

INSERT INTO Tipo_Persona (tipo_persona_id, tipo_persona_de) VALUES
    (1, 'Profesor'),
    (2, 'Alumno');

-- 10 teachers
INSERT INTO Persona (persona_ci, tipo_persona_id, persona_nom)
VALUES
    ('11111111', 1, 'María González'),
    ('22222222', 1, 'Carlos Rodríguez'),
    ('33333333', 1, 'Ana Martínez'),
    ('44444444', 1, 'José Pérez'),
    ('55555555', 1, 'Laura Sánchez'),
    ('66666666', 1, 'Miguel López'),
    ('77777777', 1, 'Sofía Ramírez'),
    ('88888888', 1, 'David Fernández'),
    ('99999999', 1, 'Elena Gómez'),
    ('10101010', 1, 'Jorge Díaz');

-- 40 students
INSERT INTO Persona (persona_ci, tipo_persona_id, persona_nom)
VALUES
    ('12121212', 2, 'Daniel Castro'),
    ('13131313', 2, 'Patricia Ruiz'),
    ('14141414', 2, 'Alejandro Jiménez'),
    ('15151515', 2, 'Lucía Herrera'),
    ('16161616', 2, 'Francisco Morales'),
    ('17171717', 2, 'Carmen Ortega'),
    ('18181818', 2, 'Roberto Vargas'),
    ('19191919', 2, 'Isabel Mendoza'),
    ('20202020', 2, 'Fernando Silva'),
    ('21212121', 2, 'Adriana Rojas'),
    ('23232323', 2, 'Ricardo Peña'),
    ('24242424', 2, 'Beatriz Guzmán'),
    ('25252525', 2, 'Hugo Navarro'),
    ('26262626', 2, 'Silvia Reyes'),
    ('27272727', 2, 'Oscar Campos'),
    ('28282828', 2, 'Natalia Soto'),
    ('29292929', 2, 'Guillermo Cortés'),
    ('30303030', 2, 'Valeria Miranda'),
    ('31313131', 2, 'Raúl Valdez'),
    ('32323232', 2, 'Gabriela Paredes'),
    ('34343434', 2, 'Mario Cabrera'),
    ('35353535', 2, 'Daniela León'),
    ('36363636', 2, 'Arturo Ríos'),
    ('37373737', 2, 'Camila Espinoza'),
    ('38383838', 2, 'Felipe Núñez'),
    ('39393939', 2, 'Marcela Vega'),
    ('40404040', 2, 'Gustavo Fuentes'),
    ('41414141', 2, 'Paulina Cordero'),
    ('42424242', 2, 'Renato Maldonado'),
    ('43434343', 2, 'Verónica Figueroa'),
    ('45454545', 2, 'Sergio Contreras'),
    ('46464646', 2, 'Ximena Pacheco'),
    ('47474747', 2, 'Héctor Santander'),
    ('48484848', 2, 'Florencia Olivares'),
    ('49494949', 2, 'Esteban Zúñiga'),
    ('50505050', 2, 'Constanza Valenzuela'),
    ('51515151', 2, 'Rodrigo Sepúlveda'),
    ('52525252', 2, 'Antonella Tapia'),
    ('53535353', 2, 'Federico Bravo'),
    ('54545454', 2, 'Alicia Parra');

-- 10 subjects
INSERT INTO Materia (materia_de)
VALUES
    ('Matematica'),
    ('Fisica'),
    ('Quimica'),
    ('Biologia'),
    ('Computacion'),
    ('Historia'),
    ('Literatura'),
    ('Arte'),
    ('Musica'),
    ('Educacion Fisica');

-- Mock data for Seccion
INSERT INTO Seccion (seccion_de)
VALUES
    ('A'),
    ('B'),
    ('C');

-- 20 Courses (15 active, 5 inactive)
INSERT INTO Curso (
    curso_de,
    materia_id,
    seccion_id,
    curso_dt_ini,
    curso_dt_end,
    curso_am_max,
    curso_profesor_ci
) VALUES
    -- INACTIVE COURSES (5) - Ended on or before 05-07-2025
    ('Matematica - 2024', 1, 1, TO_DATE('01-09-2024', 'DD-MM-YYYY'), TO_DATE('15-12-2024', 'DD-MM-YYYY'), 30, '11111111'),
    ('Fisica - 2024', 2, 2, TO_DATE('01-09-2024', 'DD-MM-YYYY'), TO_DATE('15-12-2024', 'DD-MM-YYYY'), 25, '22222222'),
    ('Quimica - 2024', 3, 3, TO_DATE('01-09-2024', 'DD-MM-YYYY'), TO_DATE('15-12-2024', 'DD-MM-YYYY'), 20, '33333333'),
    ('Biologia - 2024', 4, 1, TO_DATE('01-09-2024', 'DD-MM-YYYY'), TO_DATE('15-12-2024', 'DD-MM-YYYY'), 35, '44444444'),
    ('Computacion - 2025A', 5, 2, TO_DATE('10-01-2025', 'DD-MM-YYYY'), TO_DATE('30-06-2025', 'DD-MM-YYYY'), 40, '55555555'),

    -- ACTIVE COURSES (15) - Started on or before 05-07-2025, Ends after 05-07-2025
    ('Historia - Verano 2025', 6, 3, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('30-08-2025', 'DD-MM-YYYY'), 30, '66666666'),
    ('Literatura - Verano 2025', 7, 1, TO_DATE('15-06-2025', 'DD-MM-YYYY'), TO_DATE('30-09-2025', 'DD-MM-YYYY'), 28, '77777777'),
    ('Arte - Verano 2025', 8, 2, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('15-10-2025', 'DD-MM-YYYY'), 22, '88888888'),
    ('Musica - Otoño 2025', 9, 3, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('15-12-2025', 'DD-MM-YYYY'), 18, '99999999'),
    ('Educacion Fisica - Otoño 2025', 10, 1, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('15-12-2025', 'DD-MM-YYYY'), 30, '10101010'),
    ('Matematica - Otoño 2025', 1, 2, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 28, '11111111'),
    ('Fisica - Otoño 2025', 2, 3, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 20, '22222222'),
    ('Quimica - Otoño 2025', 3, 1, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 25, '33333333'),
    ('Biologia - Otoño 2025', 4, 2, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 30, '44444444'),
    ('Computacion - Otoño 2025', 5, 3, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 35, '55555555'),
    ('Historia - Otoño 2025', 6, 1, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 25, '66666666'),
    ('Literatura - Otoño 2025', 7, 2, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 20, '77777777'),
    ('Escultura - Otoño 2025', 8, 3, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 15, '88888888'),
    ('Musica - Otoño 2025', 9, 1, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 22, '99999999'),
    ('Educacion Fisica - Otoño 2025', 10, 2, TO_DATE('01-06-2025', 'DD-MM-YYYY'), TO_DATE('20-12-2025', 'DD-MM-YYYY'), 30, '10101010');

-- Every student is enrolled in an active course
INSERT INTO Inscripcion (curso_id, alumno_ci, inscripcion_dt)
VALUES
    (6, '12121212', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (7, '13131313', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (8, '14141414', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (9, '15151515', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (10, '16161616', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (11, '17171717', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (12, '18181818', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (13, '19191919', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (14, '20202020', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (15, '21212121', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (16, '23232323', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (17, '24242424', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (18, '25252525', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (19, '26262626', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (20, '27272727', TO_DATE('10-05-2025', 'DD-MM-YYYY')),
    (6, '28282828', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (7, '29292929', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (8, '30303030', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (9, '31313131', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (10, '32323232', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (11, '34343434', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (12, '35353535', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (13, '36363636', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (14, '37373737', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (15, '38383838', TO_DATE('15-05-2025', 'DD-MM-YYYY')),
    (16, '39393939', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (17, '40404040', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (18, '41414141', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (19, '42424242', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (20, '43434343', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (6, '45454545', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (7, '46464646', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (8, '47474747', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (9, '48484848', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (10, '49494949', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (11, '50505050', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (12, '51515151', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (13, '52525252', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (14, '53535353', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (15, '54545454', TO_DATE('20-05-2025', 'DD-MM-YYYY')),
    (16, '12121212', TO_DATE('25-05-2025', 'DD-MM-YYYY')),
    (17, '13131313', TO_DATE('25-05-2025', 'DD-MM-YYYY')),
    (18, '14141414', TO_DATE('25-05-2025', 'DD-MM-YYYY')),
    (19, '15151515', TO_DATE('25-05-2025', 'DD-MM-YYYY')),
    (20, '16161616', TO_DATE('25-05-2025', 'DD-MM-YYYY'));

-- Every enrollment has at least one grade
INSERT INTO Calificacion (inscripcion_id, calificacion_am)
VALUES
    (1, 15.5),
    (2, 12.0),
    (3, 19.1),
    (4, 10.0),
    (5, 13.5),
    (6, 19.5),
    (7, 18.2),
    (8, 14.0),
    (9, 9.5),
    (10, 16.0),
    (11, 11.0),
    (12, 19.9),
    (13, 17.0),
    (14, 12.5),
    (15, 8.0),
    (16, 18.8),
    (17, 11.5),
    (18, 19.0),
    (19, 10.5),
    (20, 16.8),
    (21, 13.0),
    (22, 19.2),
    (23, 10.8),
    (24, 17.5),
    (25, 9.0),
    (26, 19.3),
    (27, 14.5),
    (28, 11.2),
    (29, 20.0),
    (30, 8.5),
    (31, 17.8),
    (32, 13.8),
    (33, 19.7),
    (34, 7.5),
    (35, 16.5),
    (36, 10.0),
    (37, 18.0),
    (38, 14.2),
    (39, 9.2),
    (40, 17.2),
    (41, 11.8),
    (42, 19.6),
    (43, 8.2),
    (44, 15.0),
    (45, 12.2);

COMMIT;