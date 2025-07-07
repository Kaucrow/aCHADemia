BEGIN;

CREATE TABLE IF NOT EXISTS Tipo_Persona (
    tipo_persona_id INT PRIMARY KEY NOT NULL,
    tipo_persona_de VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Persona (
    persona_ci INT PRIMARY KEY NOT NULL,
    tipo_persona_id SERIAl NOT NULL,
    persona_nom VARCHAR(100) NOT NULL,

    FOREIGN KEY (tipo_persona_id) REFERENCES Tipo_Persona(tipo_persona_id)
);

CREATE TABLE IF NOT EXISTS Materia (
    materia_id SERIAL PRIMARY KEY NOT NULL,
    materia_de VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Seccion (
    seccion_id SERIAL PRIMARY KEY NOT NULL,
    seccion_de VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Curso (
    curso_id SERIAL PRIMARY KEY NOT NULL,
    curso_de VARCHAR(100) NOT NULL,
    materia_id INT NOT NULL,
    seccion_id INT NOT NULL,
    curso_dt_ini DATE NOT NULL,
    curso_dt_end DATE NOT NULL,
    curso_am_max INT NOT NULL,
    curso_profesor_ci INT NOT NULL,

    FOREIGN KEY (curso_profesor_ci) REFERENCES Persona(persona_ci),
    FOREIGN KEY (materia_id) REFERENCES Materia(materia_id),
    FOREIGN KEY (seccion_id) REFERENCES Seccion(seccion_id)
);

CREATE TABLE IF NOT EXISTS Inscripcion (
    inscripcion_id SERIAL PRIMARY KEY NOT NULL,
    curso_id INT NOT NULL,
    alumno_ci INT NOT NULL,
    inscripcion_dt DATE NOT NULL,
    
    FOREIGN KEY (curso_id) REFERENCES Curso(curso_id),
    FOREIGN KEY (alumno_ci) REFERENCES Persona(persona_ci)
);

CREATE TABLE IF NOT EXISTS Calificacion (
    calificacion_id SERIAL PRIMARY KEY NOT NULL,
    inscripcion_id INT NOT NULL,
    calificacion_am FLOAT NOT NULL,

    FOREIGN KEY (inscripcion_id) REFERENCES Inscripcion(inscripcion_id)
);

COMMIT;