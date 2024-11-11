-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 11-11-2024 a las 22:15:24
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliariafinal`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `IdContrato` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL,
  `IdInquilino` int(11) NOT NULL,
  `FechaInicio` datetime(6) NOT NULL,
  `FechaFin` datetime(6) NOT NULL,
  `MontoAlquiler` decimal(65,30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`IdContrato`, `IdInmueble`, `IdInquilino`, `FechaInicio`, `FechaFin`, `MontoAlquiler`) VALUES
(1, 1, 1, '2024-01-01 00:00:00.000000', '2026-01-01 00:00:00.000000', 50000.000000000000000000000000000000),
(2, 2, 2, '2024-01-01 00:00:00.000000', '2026-01-01 00:00:00.000000', 60000.000000000000000000000000000000),
(3, 3, 3, '2024-01-01 00:00:00.000000', '2026-01-01 00:00:00.000000', 55000.000000000000000000000000000000),
(4, 4, 4, '2024-01-01 00:00:00.000000', '2026-01-01 00:00:00.000000', 70000.000000000000000000000000000000),
(5, 5, 5, '2024-01-01 00:00:00.000000', '2026-01-01 00:00:00.000000', 65000.000000000000000000000000000000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `IdInmueble` int(11) NOT NULL,
  `Direccion` longtext NOT NULL,
  `Uso` longtext NOT NULL,
  `Tipo` longtext NOT NULL,
  `Ambientes` int(11) NOT NULL,
  `Precio` decimal(65,30) NOT NULL,
  `Estado` longtext NOT NULL,
  `IdPropietario` int(11) NOT NULL,
  `Imagen` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`IdInmueble`, `Direccion`, `Uso`, `Tipo`, `Ambientes`, `Precio`, `Estado`, `IdPropietario`, `Imagen`) VALUES
(1, 'Calla Falsa 124', 'Residencial\n', 'Departamento\n', 3, 10000.000000000000000000000000000000, 'Ocupado', 1, 'images/4e04e148-b642-4c0f-9133-36259bf64cee.jpg'),
(2, 'Av. Siempreviva 742', 'Comercial', 'Local', 1, 200000.000000000000000000000000000000, 'Ocupado', 2, 'images/53629c2f-5565-4b42-96f6-57dc16bc8fa9.jpg'),
(3, 'Calle Arbolada 457', 'Residencial', 'Casa\n', 4, 200000.000000000000000000000000000000, 'Disponible', 1, 'images/c2292ff9-5d5f-4d22-bd23-739fcc193e1c.jpg'),
(4, 'Plaza Principal 89', 'Comercial', 'Oficina', 2, 120000.000000000000000000000000000000, 'Ocupado', 2, 'images/d08b88c6-d3c2-4f1f-9993-2276b8cbfdc4.jpg'),
(5, 'Av. Libertad 68', 'Residencial', 'Departamento', 2, 90000.000000000000000000000000000000, 'Disponible', 1, 'images/ac101934-4c37-47b3-9d74-11b3c27903fd.jpg'),
(6, 'Puertas del Sol', 'Residencial\n', 'Departamento\n', 5, 250000.000000000000000000000000000000, 'Disponible', 2, 'images/b76894d6-3909-4290-bebf-c0802021e9ba.jpg'),
(10, 'Barrio Jardin', 'Residencial', 'Casa', 5, 250000.000000000000000000000000000000, 'Disponible', 2, 'images/4d68a457-e641-4f60-a221-c6ab84524db0.jpg'),
(11, 'Los quebrachos', 'Residencial', 'Casa', 5, 350000.000000000000000000000000000000, 'Disponible', 2, 'images/6a474b44-da0d-486f-a9c6-9e9c3bccf2f1.jpg'),
(12, 'barrio 1', 'Residencial ', 'Departamento ', 5, 250000.000000000000000000000000000000, 'Disponible', 5, 'images/06b4ddb0-6a95-49e2-a595-bdebd41dfaf0.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `IdInquilino` int(11) NOT NULL,
  `Dni` longtext NOT NULL,
  `NombreCompleto` longtext NOT NULL,
  `LugarTrabajo` longtext NOT NULL,
  `NombreGarante` longtext NOT NULL,
  `DniGarante` longtext NOT NULL,
  `Telefono` longtext NOT NULL,
  `Email` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`IdInquilino`, `Dni`, `NombreCompleto`, `LugarTrabajo`, `NombreGarante`, `DniGarante`, `Telefono`, `Email`) VALUES
(1, '25896314', 'María Gómez', 'Empresa ABC', 'Juan López', '36985214', '1234567890', 'maria.gomez@example.com'),
(2, '36985247', 'Carlos Pérez', 'Comercio XYZ', 'Pedro Pérez', '25896314', '0987654321', 'carlos.perez@example.com'),
(3, '14785236', 'Ana Fernández', 'Oficina MNO', 'Laura Rodríguez', '14785269', '1122334455', 'ana.fernandez@example.com'),
(4, '25896347', 'Luis Martínez', 'Consultora PQR', 'Roberto Díaz', '36985247', '6677889900', 'luis.martinez@example.com'),
(5, '78945612', 'Sofía López', 'Restaurante STU', 'Andrés García', '25874123', '5544332211', 'sofia.lopez@example.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `IdPago` int(11) NOT NULL,
  `IdContrato` int(11) NOT NULL,
  `NumeroPago` int(11) NOT NULL,
  `FechaPago` datetime(6) NOT NULL,
  `Importe` decimal(65,30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`IdPago`, `IdContrato`, `NumeroPago`, `FechaPago`, `Importe`) VALUES
(1, 1, 1, '2023-02-01 00:00:00.000000', 50000.000000000000000000000000000000),
(2, 2, 2, '2023-03-15 00:00:00.000000', 60000.000000000000000000000000000000),
(3, 3, 3, '2023-04-01 00:00:00.000000', 55000.000000000000000000000000000000),
(4, 4, 4, '2023-05-10 00:00:00.000000', 70000.000000000000000000000000000000),
(5, 5, 5, '2023-06-05 00:00:00.000000', 65000.000000000000000000000000000000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `IdPropietario` int(11) NOT NULL,
  `Dni` longtext NOT NULL,
  `Apellido` longtext NOT NULL,
  `Nombre` longtext NOT NULL,
  `Telefono` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `FotoPerfil` longtext NOT NULL,
  `ResetToken` longtext DEFAULT NULL,
  `ResetTokenExpiry` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`IdPropietario`, `Dni`, `Apellido`, `Nombre`, `Telefono`, `Email`, `Password`, `FotoPerfil`, `ResetToken`, `ResetTokenExpiry`) VALUES
(1, '12345678', 'Perez', 'Juan', '123456789', 'juan.perez@example.com2', 'securepassword', '', NULL, NULL),
(2, '33539061', 'Fernandez', 'Fermin Orlando', '2664297704', 'fermin2049@gmail.com', 'rPfNvgyddlyu/9uT8QZovdNFhKCPUs9HCJ7oF7W2wTM=', 'images/a5d111be-5e1d-445b-93fa-72f0ce30cb8a.jpg', NULL, NULL),
(3, '33555666', 'perez', 'fulano', '2664010203', 'fulano@gmail.com', 'jRygjhDRvH1SYHlmEggPGqs0Alu5Q8xLMvX1fXWeU+U=', '', NULL, NULL),
(4, '52353532', 'Fernández ', 'Tomás ', '2664497704', 'tomas@gmail.con', 'jRygjhDRvH1SYHlmEggPGqs0Alu5Q8xLMvX1fXWeU+U=', '', NULL, NULL),
(5, '33001002', 'Perez', 'Gil', '2664010203', 'perezgil@gmail.com', 'jRygjhDRvH1SYHlmEggPGqs0Alu5Q8xLMvX1fXWeU+U=', 'images/959b6c8b-54f6-48e2-8a26-b2d650981474.jpg', NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20241016143231_InitialCreate', '8.0.10'),
('20241022194144_AddFotoPerfilToPropietario', '8.0.10'),
('20241031184038_AddImagenToInmueble', '8.0.10'),
('20241104220708_AddResetTokenFieldsToPropietario', '8.0.10'),
('20241108195711_AddNewFieldToPropietario', '8.0.10'),
('20241108195832_AddDireccionToPropietario', '8.0.10');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`IdContrato`),
  ADD KEY `IX_Contratos_IdInmueble` (`IdInmueble`),
  ADD KEY `IX_Contratos_IdInquilino` (`IdInquilino`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`IdInmueble`),
  ADD UNIQUE KEY `Direccion` (`Direccion`) USING HASH,
  ADD KEY `IX_Inmuebles_IdPropietario` (`IdPropietario`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`IdInquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`IdPago`),
  ADD KEY `IX_Pagos_IdContrato` (`IdContrato`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`IdPropietario`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `IdContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `IdInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `IdInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `IdPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `IdPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `FK_Contratos_Inmuebles_IdInmueble` FOREIGN KEY (`IdInmueble`) REFERENCES `inmuebles` (`IdInmueble`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Contratos_Inquilinos_IdInquilino` FOREIGN KEY (`IdInquilino`) REFERENCES `inquilinos` (`IdInquilino`) ON DELETE CASCADE;

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `FK_Inmuebles_Propietarios_IdPropietario` FOREIGN KEY (`IdPropietario`) REFERENCES `propietarios` (`IdPropietario`) ON DELETE CASCADE;

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `FK_Pagos_Contratos_IdContrato` FOREIGN KEY (`IdContrato`) REFERENCES `contratos` (`IdContrato`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
