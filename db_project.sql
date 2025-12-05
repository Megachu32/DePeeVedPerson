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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `customers` */

insert  into `customers`(`customer_id`,`name`,`phone`,`email`,`government_id`,`created_at`) values 
(1,'John Smith','0812345678','john@mail.com','ID83922191','2025-11-26 18:05:30'),
(2,'Emily Clark','0823456789','emily@mail.com','ID88412222','2025-11-26 18:05:30'),
(3,'Michael Brown','0834567891','michael@mail.com','ID77291823','2025-11-26 18:05:30'),
(4,'Sarah Wilson','0845678912','sarah@mail.com','ID61588294','2025-11-26 18:05:30');

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
(1,15),
(2,18),
(3,7),
(4,10),
(5,22);

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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `preorders` */

insert  into `preorders`(`preorder_id`,`customer_id`,`product_id`,`preorder_date`,`status`,`money_hold_amount`,`final_charge_amount`,`payment_method`,`pickup_code`,`reserved_for_pickup_until`,`pickup_date`,`cancellation_reason`) values 
(1,4,4,'2025-11-26 18:05:30','order_placed',300.00,1499.00,'credit_card','PUK-2911','2025-12-01 18:05:30',NULL,NULL),
(2,2,1,'2025-11-26 18:05:30','arrived',400.00,1699.00,'cash','PUL-8831','2025-11-29 18:05:30','2025-11-26 18:05:30',NULL),
(3,3,5,'2025-11-26 18:05:30','canceled',200.00,NULL,'debit','PUC-4412',NULL,NULL,'Customer changed mind');

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
(8,'sdsdsd','MAKANAn','dsdsds','dsds',12,'2025-11-30',12.00,'sdsd','dsds','dsdsd','active','dsd'),
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
  PRIMARY KEY (`sale_item_id`),
  KEY `sale_id` (`sale_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `sale_items_ibfk_1` FOREIGN KEY (`sale_id`) REFERENCES `sales` (`sale_id`),
  CONSTRAINT `sale_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `sale_items` */

insert  into `sale_items`(`sale_item_id`,`sale_id`,`product_id`,`quantity`,`unit_price`) values 
(1,1,1,1,1699.00),
(2,2,2,1,999.00),
(3,3,3,1,1249.00);

/*Table structure for table `sales` */

DROP TABLE IF EXISTS `sales`;

CREATE TABLE `sales` (
  `sale_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) DEFAULT NULL,
  `sale_date` datetime DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  `tax` decimal(10,2) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`sale_id`),
  KEY `customer_id` (`customer_id`),
  CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `sales` */

insert  into `sales`(`sale_id`,`customer_id`,`sale_date`,`subtotal`,`tax`,`total`) values 
(1,1,'2025-11-26 18:05:30',1699.00,169.90,1868.90),
(2,2,'2025-11-26 18:05:30',999.00,99.90,1098.90),
(3,3,'2025-11-26 18:05:30',1249.00,124.90,1373.90);

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
  PRIMARY KEY (`staff_id`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `staff` */

insert  into `staff`(`staff_id`,`name`,`email`,`phone`,`username`,`password`,`role`,`hire_date`,`status`) values 
(1,'Alice Johnson','alice@shop.com','0811111111','alice','hashed_pw_1','admin','2023-01-10','active'),
(2,'Bob Carter','bob@shop.com','0822222222','bob','hashed_pw_2','cashier','2023-03-15','active'),
(3,'Charlie Evans','charlie@shop.com','0833333333','charlie','hashed_pw_3','technician','2023-05-20','inactive'),
(4,'Diana Brooks','diana@shop.com','0844444444','diana','hashed_pw_4','manager','2024-02-01','active');

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
