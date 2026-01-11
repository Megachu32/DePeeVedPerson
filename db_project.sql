/*
SQLyog Community v13.3.0 (64 bit)
MySQL - 10.4.32-MariaDB : Database - db_project
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`db_project` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `db_project`;

/*Table structure for table `customers` */

DROP TABLE IF EXISTS `customers`;

CREATE TABLE `customers` (
  `customer_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `government_id` varchar(50) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `customers` */

insert  into `customers`(`customer_id`,`name`,`phone`,`email`,`government_id`,`created_at`) values 
(1,'John Smith','0812345678','john@mail.com','ID83922191','2025-11-26 18:05:30'),
(2,'Emily Clark','0823456789','emily@mail.com','ID88412222','2025-11-26 18:05:30'),
(3,'Michael Brown','0834567891','michael@mail.com','ID77291823','2025-11-26 18:05:30'),
(4,'Sarah Wilson','0845678912','sarah@mail.com','ID61588294','2025-11-26 18:05:30'),
(5,'Online','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(6,'Walk-in','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(7,'Online','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(8,'Online','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(9,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-04 21:43:24'),
(10,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-04 21:43:24'),
(11,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-04 21:48:07'),
(12,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-04 21:53:58'),
(13,'Online','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(14,'Online','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(15,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-04 23:15:26'),
(16,'Walk-in','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(17,'Walk-in','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(18,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-04 23:21:38'),
(19,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-04 23:23:40'),
(20,'Walk-in','111111111','lolme@gmail.com','000-000-0000','0001-01-01 00:00:00'),
(21,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-04 23:30:10'),
(22,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:12:49'),
(23,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:18:37'),
(24,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:21:40'),
(25,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:22:11'),
(26,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:22:58'),
(27,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:30:25'),
(28,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:31:12'),
(29,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-05 18:33:23'),
(30,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 18:38:16'),
(31,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 18:39:49'),
(32,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-06 18:44:38'),
(33,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 18:46:25'),
(34,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 18:47:21'),
(35,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:00:37'),
(36,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:06:34'),
(37,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:08:24'),
(38,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:16:50'),
(39,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:21:36'),
(40,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:41:48'),
(41,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:52:18'),
(42,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 19:58:43'),
(43,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 20:05:06'),
(44,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 20:09:46'),
(45,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-06 20:12:02'),
(46,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 20:34:22'),
(47,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-06 20:39:00'),
(48,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-06 20:44:55'),
(49,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-07 13:35:43'),
(50,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-07 13:48:31'),
(51,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-07 14:01:44'),
(52,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-07 14:03:55'),
(53,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-07 14:05:24'),
(54,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-07 14:08:29'),
(55,'Walk-in','111111111','lolme@gmail.com','000-000-0000','2026-01-07 14:56:41'),
(56,'Online','111111111','lolme@gmail.com','000-000-0000','2026-01-07 20:47:29');

/*Table structure for table `discounts` */

DROP TABLE IF EXISTS `discounts`;

CREATE TABLE `discounts` (
  `discount_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `discount_percentage` int(11) NOT NULL,
  `start_date` date DEFAULT NULL,
  `end_date` date DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`discount_id`),
  KEY `fk_discounts_product` (`product_id`),
  CONSTRAINT `fk_discounts_product` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `discounts` */

insert  into `discounts`(`discount_id`,`product_id`,`discount_percentage`,`start_date`,`end_date`,`is_active`) values 
(1,1,10,'2025-12-01','2025-12-31',1),
(2,1,20,NULL,NULL,1),
(3,4,30,'2025-11-01','2025-11-30',1);

/*Table structure for table `inventory` */

DROP TABLE IF EXISTS `inventory`;

CREATE TABLE `inventory` (
  `product_id` int(11) NOT NULL,
  `stock` int(11) DEFAULT NULL,
  PRIMARY KEY (`product_id`),
  CONSTRAINT `inventory_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `inventory` */

insert  into `inventory`(`product_id`,`stock`) values 
(1,100),
(2,100),
(3,100),
(4,0),
(5,9),
(8,0),
(11,0),
(12,100);

/*Table structure for table `preorders` */

DROP TABLE IF EXISTS `preorders`;

CREATE TABLE `preorders` (
  `preorder_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) DEFAULT NULL,
  `product_id` int(11) DEFAULT NULL,
  `preorder_date` datetime DEFAULT NULL,
  `status` enum('order_placed','arrived','picked_up','canceled') DEFAULT NULL,
  `money_hold_amount` decimal(10,2) DEFAULT NULL,
  `final_charge_amount` decimal(10,2) DEFAULT NULL,
  `payment_method` varchar(50) DEFAULT NULL,
  `pickup_code` varchar(50) DEFAULT NULL,
  `reserved_for_pickup_until` datetime DEFAULT NULL,
  `pickup_date` datetime DEFAULT NULL,
  `cancellation_reason` text DEFAULT NULL,
  PRIMARY KEY (`preorder_id`),
  KEY `customer_id` (`customer_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `preorders_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  CONSTRAINT `preorders_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `preorders` */

insert  into `preorders`(`preorder_id`,`customer_id`,`product_id`,`preorder_date`,`status`,`money_hold_amount`,`final_charge_amount`,`payment_method`,`pickup_code`,`reserved_for_pickup_until`,`pickup_date`,`cancellation_reason`) values 
(1,4,4,'2025-11-26 18:05:30','order_placed',300.00,1499.00,'credit_card','PUK-2911','2025-12-01 18:05:30',NULL,NULL),
(2,2,1,'2025-11-26 18:05:30','arrived',400.00,1699.00,'cash','PUL-8831','2025-11-29 18:05:30','2025-11-26 18:05:30',NULL),
(3,3,5,'2025-11-26 18:05:30','canceled',200.00,NULL,'debit','PUC-4412',NULL,NULL,'Customer changed mind'),
(4,12,8,'2026-01-04 21:53:58','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(10,18,8,'2026-01-04 23:21:38','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(11,19,8,'2026-01-04 23:23:40','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(12,20,8,'0001-01-01 00:00:00','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(13,22,8,'2026-01-05 18:12:49','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(14,27,8,'2026-01-05 18:30:25','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(15,28,8,'2026-01-05 18:31:12','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(16,29,8,'2026-01-05 18:33:23','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(17,30,8,'2026-01-06 18:38:16','order_placed',1.20,NULL,'cash',NULL,NULL,NULL,NULL),
(18,31,8,'2026-01-06 18:39:49','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(19,32,8,'2026-01-06 18:44:38','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(20,33,8,'2026-01-06 18:46:25','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(21,34,8,'2026-01-06 18:47:21','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(22,35,8,'2026-01-06 19:00:37','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(23,36,8,'2026-01-06 19:06:34','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(24,37,8,'2026-01-06 19:08:24','order_placed',1.20,NULL,'cash',NULL,NULL,NULL,NULL),
(25,38,8,'2026-01-06 19:16:50','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(26,39,8,'2026-01-06 19:21:36','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(27,40,8,'2026-01-06 19:41:48','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(28,41,8,'2026-01-06 19:52:18','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(29,43,8,'2026-01-06 20:05:06','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(30,44,8,'2026-01-06 20:09:46','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(31,45,8,'2026-01-06 20:12:02','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(32,46,8,'2026-01-06 20:34:22','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(33,49,8,'2026-01-07 13:35:43','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(34,50,8,'2026-01-07 13:48:31','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(35,51,8,'2026-01-07 14:01:44','order_placed',1.20,NULL,'credit card',NULL,NULL,NULL,NULL),
(36,55,8,'2026-01-07 14:56:41','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL),
(37,56,8,'2026-01-07 20:47:29','order_placed',1.20,NULL,'e-wallet',NULL,NULL,NULL,NULL);

/*Table structure for table `products` */

DROP TABLE IF EXISTS `products`;

CREATE TABLE `products` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `sku` varchar(50) DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `model` varchar(50) DEFAULT NULL,
  `generation` int(11) DEFAULT NULL,
  `release_date` date DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `color` varchar(50) DEFAULT NULL,
  `storage` varchar(50) DEFAULT NULL,
  `specifications` text DEFAULT NULL,
  `status` enum('active','inactive','incoming') DEFAULT 'active',
  `description` text DEFAULT NULL,
  PRIMARY KEY (`product_id`),
  UNIQUE KEY `sku` (`sku`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `products` */

insert  into `products`(`product_id`,`sku`,`name`,`type`,`model`,`generation`,`release_date`,`price`,`color`,`storage`,`specifications`,`status`,`description`) values 
(1,'IP16PM-256BLK','iPhone 16 Pro Max','iPhone','A3102',16,'2024-09-20',24990000.00,'Black Titanium','256GB','A18 Pro chip, 120Hz display, 5-camera system','active','Latest flagship iPhone'),
(2,'IP15-128BLU','iPhone 15','iPhone','A3085',15,'2023-09-22',15990000.00,'Blue','128GB','A16 Bionic, 48MP main camera','active','Standard iPhone 15'),
(3,'IPADP11-256SLV','iPad Pro 11\"','iPad','A2759',5,'2022-10-15',18990000.00,'Silver','256GB','M2 chip, 120Hz ProMotion, Face ID','active','iPad Pro 11 inch (2022)'),
(4,'MACAR15-512GRY','MacBook Air 15\"','MacBook','A2941',1,'2023-06-13',23990000.00,'Space Gray','512GB','M2 chip, Liquid Retina display','active','MacBook Air 15-inch'),
(5,'MACPRO14-1TBBLK','MacBook Pro 14\"','MacBook','A2992',3,'2023-11-07',38990000.00,'Black','1TB','M3 Pro chip, 120Hz XDR display','active','MacBook Pro 2023'),
(8,'sdsdsd','MAKANAn','dsdsds','dsds',12,'2025-11-30',12.00,'sdsd','dsds','dsdsd','incoming','dsd'),
(11,'idhiufa','aousihriqu','MacBook','aiufhq',12,'2025-12-01',1222.00,'asas','sas','asas','active','asasasas'),
(12,'LKosdf','Iphone 20','iPhone','A121',20,'2027-07-09',1233.00,'Black','230Mb','sdfghsdfg','active','makanan');

/*Table structure for table `sale_items` */

DROP TABLE IF EXISTS `sale_items`;

CREATE TABLE `sale_items` (
  `sale_item_id` int(11) NOT NULL AUTO_INCREMENT,
  `sale_id` int(11) DEFAULT NULL,
  `product_id` int(11) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `unit_price` decimal(10,2) DEFAULT NULL,
  `discount_amount` decimal(10,2) DEFAULT 0.00,
  `order_mode` enum('normal','pre-order') NOT NULL DEFAULT 'normal',
  `preorder_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`sale_item_id`),
  KEY `sale_id` (`sale_id`),
  KEY `product_id` (`product_id`),
  KEY `fk_sale_items_preorder` (`preorder_id`),
  CONSTRAINT `fk_sale_items_preorder` FOREIGN KEY (`preorder_id`) REFERENCES `preorders` (`preorder_id`) ON DELETE SET NULL,
  CONSTRAINT `sale_items_ibfk_1` FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`),
  CONSTRAINT `sale_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=86 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `sale_items` */

insert  into `sale_items`(`sale_item_id`,`sale_id`,`product_id`,`quantity`,`unit_price`,`discount_amount`,`order_mode`,`preorder_id`) values 
(1,1,1,1,1699.00,0.00,'normal',NULL),
(2,2,2,1,999.00,0.00,'normal',NULL),
(3,3,3,1,1249.00,0.00,'normal',NULL),
(4,8,8,14,12.00,0.00,'normal',NULL),
(5,8,4,1,23990000.00,7197000.00,'normal',NULL),
(9,14,8,12,12.00,0.00,'normal',10),
(10,14,4,2,23990000.00,14394000.00,'normal',NULL),
(11,15,8,2,12.00,0.00,'normal',11),
(12,15,3,2,18990000.00,0.00,'normal',NULL),
(13,16,8,12,12.00,0.00,'normal',12),
(14,17,4,2,23990000.00,14394000.00,'normal',NULL),
(15,18,8,121212,12.00,0.00,'normal',13),
(16,19,2,2,15990000.00,0.00,'normal',NULL),
(17,20,2,2,15990000.00,0.00,'normal',NULL),
(18,21,4,2,23990000.00,14394000.00,'normal',NULL),
(19,22,3,1,18990000.00,0.00,'normal',NULL),
(20,23,8,22,12.00,0.00,'normal',14),
(21,24,8,121212,12.00,0.00,'normal',15),
(22,25,8,12,12.00,0.00,'normal',16),
(23,26,8,12,12.00,0.00,'normal',17),
(24,27,8,1,12.00,0.00,'normal',18),
(25,28,8,2,12.00,0.00,'normal',19),
(26,29,8,10,12.00,0.00,'normal',20),
(27,30,8,2,12.00,0.00,'normal',21),
(28,31,8,1,12.00,0.00,'normal',22),
(29,32,1,2,24990000.00,4998000.00,'normal',NULL),
(30,32,2,1,15990000.00,0.00,'normal',NULL),
(31,32,4,1,23990000.00,7197000.00,'normal',NULL),
(32,32,8,2,12.00,0.00,'normal',23),
(33,33,8,3,12.00,0.00,'normal',24),
(34,33,3,1,18990000.00,0.00,'normal',NULL),
(35,33,5,2,38990000.00,0.00,'normal',NULL),
(36,34,8,4,12.00,0.00,'normal',25),
(37,34,5,1,38990000.00,0.00,'normal',NULL),
(38,35,8,1,12.00,0.00,'normal',26),
(39,35,5,1,38990000.00,0.00,'normal',NULL),
(40,35,4,1,23990000.00,7197000.00,'normal',NULL),
(41,36,8,1,12.00,0.00,'normal',27),
(42,36,3,1,18990000.00,0.00,'normal',NULL),
(43,37,8,2,12.00,0.00,'normal',28),
(44,37,5,1,38990000.00,0.00,'normal',NULL),
(45,37,1,1,24990000.00,2499000.00,'normal',NULL),
(46,38,2,2,15990000.00,0.00,'normal',NULL),
(47,38,4,4,23990000.00,28788000.00,'normal',NULL),
(48,38,1,1,24990000.00,2499000.00,'normal',NULL),
(49,39,1,1,24990000.00,2499000.00,'normal',NULL),
(50,39,2,1,15990000.00,0.00,'normal',NULL),
(51,39,3,1,18990000.00,0.00,'normal',NULL),
(52,39,8,1,12.00,0.00,'normal',29),
(53,40,1,1,24990000.00,2499000.00,'normal',NULL),
(54,40,2,1,15990000.00,0.00,'normal',NULL),
(55,40,4,1,23990000.00,7197000.00,'normal',NULL),
(56,40,8,1,12.00,0.00,'normal',30),
(57,41,1,1,24990000.00,2499000.00,'normal',NULL),
(58,41,3,1,18990000.00,0.00,'normal',NULL),
(59,41,2,1,15990000.00,0.00,'normal',NULL),
(60,41,4,1,23990000.00,7197000.00,'normal',NULL),
(61,41,8,1,12.00,0.00,'normal',31),
(62,42,1,3,24990000.00,7497000.00,'normal',NULL),
(63,42,8,1,12.00,0.00,'normal',32),
(64,43,2,2,15990000.00,0.00,'normal',NULL),
(65,44,1,3,24990000.00,7497000.00,'normal',NULL),
(66,44,3,2,18990000.00,0.00,'normal',NULL),
(67,45,2,2,15990000.00,0.00,'normal',NULL),
(68,45,4,1,23990000.00,7197000.00,'normal',NULL),
(69,45,8,1,12.00,0.00,'normal',33),
(70,46,8,1,12.00,0.00,'normal',34),
(71,46,4,1,23990000.00,7197000.00,'normal',NULL),
(72,46,1,1,24990000.00,2499000.00,'normal',NULL),
(73,46,2,1,15990000.00,0.00,'normal',NULL),
(74,47,1,1,24990000.00,2499000.00,'normal',NULL),
(75,47,8,14,12.00,0.00,'normal',35),
(76,48,2,3,15990000.00,0.00,'normal',NULL),
(77,48,4,1,23990000.00,7197000.00,'normal',NULL),
(78,48,5,1,38990000.00,0.00,'normal',NULL),
(79,49,3,6,18990000.00,0.00,'normal',NULL),
(80,49,4,1,23990000.00,7197000.00,'normal',NULL),
(81,50,5,3,38990000.00,0.00,'normal',NULL),
(82,51,5,2,38990000.00,0.00,'normal',NULL),
(83,51,8,1,12.00,0.00,'normal',36),
(84,52,5,2,38990000.00,0.00,'normal',NULL),
(85,52,8,10,12.00,0.00,'normal',37);

/*Table structure for table `sales` */

DROP TABLE IF EXISTS `sales`;

CREATE TABLE `sales` (
  `sale_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) DEFAULT NULL,
  `customer_ref` varchar(100) DEFAULT NULL,
  `sale_date` datetime DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  `tax` decimal(10,2) DEFAULT NULL,
  `discount_amount` decimal(10,2) DEFAULT 0.00,
  `total` decimal(10,2) DEFAULT NULL,
  `store_id` int(11) DEFAULT NULL,
  `payment_method` varchar(50) DEFAULT NULL,
  `purchase_type` enum('online','offline') DEFAULT 'offline',
  `pickup_method` enum('online','offline') DEFAULT 'offline',
  `order_mode` enum('normal','pre-order') DEFAULT 'normal',
  PRIMARY KEY (`sale_id`),
  KEY `customer_id` (`customer_id`),
  KEY `fk_sales_store` (`store_id`),
  CONSTRAINT `fk_sales_store` FOREIGN KEY (`store_id`) REFERENCES `stores` (`store_id`),
  CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `sales` */

insert  into `sales`(`sale_id`,`customer_id`,`customer_ref`,`sale_date`,`subtotal`,`tax`,`discount_amount`,`total`,`store_id`,`payment_method`,`purchase_type`,`pickup_method`,`order_mode`) values 
(1,1,NULL,'2025-11-26 18:05:30',1699.00,169.90,0.00,1868.90,NULL,NULL,'offline','offline','normal'),
(2,2,NULL,'2025-11-26 18:05:30',999.00,99.90,0.00,1098.90,NULL,NULL,'offline','offline','normal'),
(3,3,NULL,'2025-11-26 18:05:30',1249.00,124.90,0.00,1373.90,NULL,NULL,'offline','offline','normal'),
(4,8,'ONL8','0001-01-01 00:00:00',NULL,NULL,0.00,144.00,1,'cash','online','offline','normal'),
(5,9,'WLK9','2026-01-04 21:43:24',NULL,NULL,0.00,22491000.00,1,'cash','offline','offline','normal'),
(6,10,'WLK10','2026-01-04 21:43:24',NULL,NULL,0.00,22491000.00,1,'cash','offline','offline','normal'),
(7,11,'ONL11','2026-01-04 21:48:07',NULL,NULL,0.00,144.00,1,'credit card','online','offline','normal'),
(8,12,'WLK12','2026-01-04 21:53:58',NULL,NULL,0.00,144.00,1,'credit card','offline','online','normal'),
(9,13,'ONL13','0001-01-01 00:00:00',NULL,NULL,0.00,144.00,1,'cash','online','offline','normal'),
(10,14,'ONL14','0001-01-01 00:00:00',NULL,NULL,0.00,144.00,1,'cash','online','offline','normal'),
(11,15,'ONL15','2026-01-04 23:15:26',NULL,NULL,0.00,144.00,1,'e-wallet','online','offline','normal'),
(12,16,'WLK16','0001-01-01 00:00:00',NULL,NULL,0.00,144.00,1,'e-wallet','offline','online','normal'),
(13,17,'WLK17','0001-01-01 00:00:00',NULL,NULL,0.00,144.00,1,'e-wallet','offline','online','normal'),
(14,18,'ONL18','2026-01-04 23:21:38',NULL,NULL,0.00,144.00,1,'e-wallet','online','offline','normal'),
(15,19,'ONL19','2026-01-04 23:23:40',NULL,NULL,0.00,24.00,1,'e-wallet','online','offline','normal'),
(16,20,'WLK20','0001-01-01 00:00:00',NULL,NULL,0.00,144.00,1,'credit card','offline','offline','normal'),
(17,21,'ONL21','2026-01-04 23:30:10',NULL,NULL,0.00,33586000.00,1,'e-wallet','online','offline','normal'),
(18,22,'ONL22','2026-01-05 18:12:49',NULL,NULL,0.00,1454544.00,1,'credit card','online','offline','normal'),
(19,23,'ONL23','2026-01-05 18:18:37',NULL,NULL,0.00,31980000.00,1,'credit card','online','offline','normal'),
(20,24,'WLK24','2026-01-05 18:21:40',NULL,NULL,0.00,31980000.00,1,'cash','offline','online','normal'),
(21,25,'WLK25','2026-01-05 18:22:11',NULL,NULL,0.00,33586000.00,1,'cash','offline','online','normal'),
(22,26,'WLK26','2026-01-05 18:22:58',NULL,NULL,0.00,18990000.00,1,'credit card','offline','online','normal'),
(23,27,'ONL27','2026-01-05 18:30:25',NULL,NULL,0.00,264.00,1,'credit card','online','offline','normal'),
(24,28,'WLK28','2026-01-05 18:31:12',NULL,NULL,0.00,1454544.00,1,'credit card','offline','online','normal'),
(25,29,'WLK29','2026-01-05 18:33:23',NULL,NULL,0.00,144.00,1,'e-wallet','offline','online','normal'),
(26,30,'ONL30','2026-01-06 18:38:16',NULL,NULL,0.00,144.00,1,'cash','online','offline','normal'),
(27,31,'ONL31','2026-01-06 18:39:49',NULL,NULL,0.00,12.00,1,'credit card','online','offline','normal'),
(28,32,'WLK32','2026-01-06 18:44:38',NULL,NULL,0.00,24.00,1,'e-wallet','offline','online','normal'),
(29,33,'ONL33','2026-01-06 18:46:25',NULL,NULL,0.00,120.00,1,'credit card','online','online','normal'),
(30,34,'ONL34','2026-01-06 18:47:21',NULL,NULL,0.00,24.00,1,'credit card','online','online','normal'),
(31,35,'WLK35','2026-01-06 19:00:37',NULL,NULL,0.00,12.00,1,'credit card','offline','online','normal'),
(32,36,'ONL36','2026-01-06 19:06:34',NULL,NULL,0.00,44982000.00,1,'credit card','online','offline','normal'),
(33,37,'WLK37','2026-01-06 19:08:24',NULL,NULL,0.00,36.00,1,'cash','offline','online','normal'),
(34,38,'ONL38','2026-01-06 19:16:50',NULL,NULL,0.00,48.00,1,'credit card','online','offline','normal'),
(35,39,'ONL39','2026-01-06 19:21:36',NULL,NULL,0.00,12.00,1,'credit card','online','offline','normal'),
(36,40,'WLK40','2026-01-06 19:41:48',NULL,NULL,0.00,12.00,1,'credit card','offline','online','normal'),
(37,41,'ONL41','2026-01-06 19:52:18',NULL,NULL,0.00,24.00,1,'credit card','online','online','normal'),
(38,42,'ONL42','2026-01-06 19:58:43',NULL,NULL,0.00,31980000.00,1,'credit card','online','offline','normal'),
(39,43,'ONL43','2026-01-06 20:05:06',NULL,NULL,0.00,22491000.00,1,'credit card','online','online','normal'),
(40,44,'ONL44','2026-01-06 20:09:46',NULL,NULL,0.00,22491000.00,1,'e-wallet','online','offline','normal'),
(41,45,'WLK45','2026-01-06 20:12:02',NULL,NULL,0.00,22491000.00,1,'e-wallet','offline','online','normal'),
(42,46,'ONL46','2026-01-06 20:34:22',NULL,NULL,0.00,67473000.00,1,'credit card','online','offline','normal'),
(43,47,'ONL47','2026-01-06 20:39:00',NULL,NULL,0.00,31980000.00,1,'credit card','online','offline','normal'),
(44,48,'WLK48','2026-01-06 20:44:55',NULL,NULL,0.00,67473000.00,1,'credit card','offline','offline','normal'),
(45,49,'ONL49','2026-01-07 13:35:43',NULL,NULL,0.00,31980000.00,1,'credit card','online','offline','normal'),
(46,50,'WLK50','2026-01-07 13:48:31',NULL,NULL,0.00,12.00,1,'e-wallet','offline','offline','normal'),
(47,51,'WLK51','2026-01-07 14:01:44',NULL,NULL,0.00,22491168.00,1,'credit card','offline','offline','normal'),
(48,52,'WLK52','2026-01-07 14:03:55',NULL,NULL,0.00,99999999.99,1,'credit card','offline','offline','normal'),
(49,53,'WLK53','2026-01-07 14:05:24',NULL,NULL,0.00,99999999.99,1,'credit card','offline','offline','normal'),
(50,54,'WLK54','2026-01-07 14:08:29',NULL,NULL,0.00,99999999.99,1,'credit card','offline','online','normal'),
(51,55,'WLK55','2026-01-07 14:56:41',NULL,NULL,0.00,77980012.00,1,'e-wallet','offline','online','normal'),
(52,56,'ONL56','2026-01-07 20:47:29',NULL,NULL,0.00,77980120.00,1,'e-wallet','online','offline','normal');

/*Table structure for table `staff` */

DROP TABLE IF EXISTS `staff`;

CREATE TABLE `staff` (
  `staff_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `role` enum('admin','cashier','technician','manager') NOT NULL,
  `hire_date` date DEFAULT NULL,
  `status` enum('active','inactive') DEFAULT 'active',
  `store_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`staff_id`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`),
  KEY `fk_staff_store` (`store_id`),
  CONSTRAINT `fk_staff_store` FOREIGN KEY (`store_id`) REFERENCES `stores` (`store_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `staff` */

insert  into `staff`(`staff_id`,`name`,`email`,`phone`,`username`,`password`,`role`,`hire_date`,`status`,`store_id`) values 
(1,'Alice Johnson','alice@shop.com','0811111111','alice','hashed_pw_1','admin','2023-01-10','active',NULL),
(2,'Bob Carter','bob@shop.com','0822222222','bob','hashed_pw_2','cashier','2023-03-15','active',1),
(3,'Charlie Evans','charlie@shop.com','0833333333','charlie','hashed_pw_3','technician','2023-05-20','inactive',NULL),
(4,'Diana Brookssd','diana@shop.com','0844444444','diana','hashed_pw_4','manager','2024-02-01','active',NULL);

/*Table structure for table `stores` */

DROP TABLE IF EXISTS `stores`;

CREATE TABLE `stores` (
  `store_id` int(11) NOT NULL AUTO_INCREMENT,
  `store_name` varchar(100) NOT NULL,
  `store_address` text DEFAULT NULL,
  `company_name` varchar(100) DEFAULT NULL,
  `customer_service_phone` varchar(20) DEFAULT NULL,
  `company_location` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`store_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `stores` */

insert  into `stores`(`store_id`,`store_name`,`store_address`,`company_name`,`customer_service_phone`,`company_location`) values 
(1,'lolme3000','asdadadjhvuehgzuywuycgeriuwtd87eygr','Big chunggus','08716271627162','JL.Ahmad Yani Gg. Lingga Sari'),
(2,'',NULL,NULL,NULL,NULL);

/*Table structure for table `trade_ins` */

DROP TABLE IF EXISTS `trade_ins`;

CREATE TABLE `trade_ins` (
  `trade_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) DEFAULT NULL,
  `device_type` varchar(50) DEFAULT NULL,
  `device_name` varchar(50) DEFAULT NULL,
  `grade` enum('excellent','good','fair','bad') DEFAULT NULL,
  `trade_value` decimal(10,2) DEFAULT NULL,
  `trade_date` date DEFAULT NULL,
  PRIMARY KEY (`trade_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `trade_ins` */

insert  into `trade_ins`(`trade_id`,`customer_id`,`device_type`,`device_name`,`grade`,`trade_value`,`trade_date`) values 
(1,1,'iPhone','iPhone 14 Pro Max','excellent',13000000.00,'2025-01-15'),
(2,2,'iPad','iPad Air 4','good',5500000.00,'2025-01-20'),
(3,3,'MacBook','MacBook Pro 2019','fair',9500000.00,'2025-01-25');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
