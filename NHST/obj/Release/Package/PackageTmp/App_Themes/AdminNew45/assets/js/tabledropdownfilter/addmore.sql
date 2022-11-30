-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 04, 2017 at 06:09 AM
-- Server version: 10.1.21-MariaDB
-- PHP Version: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mydata`
--

-- --------------------------------------------------------

--
-- Table structure for table `addmore`
--

CREATE TABLE `addmore` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(45) NOT NULL,
  `user_gender` enum('male','female') NOT NULL,
  `user_address` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `addmore`
--

INSERT INTO `addmore` (`user_id`, `user_name`, `user_gender`, `user_address`) VALUES
(15, 'Tedir', 'male', 'Pidie'),
(16, 'Ghazali', 'male', 'Pijay'),
(17, 'Samudra', 'male', 'Langsa'),
(18, 'Nurjannah', 'female', 'Daya'),
(19, 'Akmal', 'male', 'Indrajaya'),
(20, 'Nurwati', 'male', 'Meulaboh'),
(21, 'Tedir Ghazali', 'male', 'Pidie'),
(22, 'Nurjannah', 'female', 'Pijay'),
(23, 'Akhmal', 'male', 'Banda'),
(24, 'Kuwe', 'female', 'Binjay');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `addmore`
--
ALTER TABLE `addmore`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `addmore`
--
ALTER TABLE `addmore`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
