-- ----------------------------
-- Table structure for test_user
-- ----------------------------
DROP TABLE IF EXISTS `test_user`;
CREATE TABLE `test_user`  (
  `id` varchar(36) PRIMARY KEY NOT NULL,
  `name` varchar(128) DEFAULT NULL,
  `age` int(11) NULL DEFAULT NULL,
  `born` datetime(0) NULL DEFAULT NULL,
  `descrption` varchar(128) DEFAULT NULL
)

-- ----------------------------
-- Records of test_user
-- ----------------------------
INSERT INTO `test_user` VALUES ('1', 'Tom', 23, '1994-10-20 00:00:00', 'Cheerful person');
INSERT INTO `test_user` VALUES ('2', 'Sam', 18, '1999-01-21 00:00:00', 'College student');
INSERT INTO `test_user` VALUES ('3', 'Julie', 18, '1999-05-21 00:00:00', 'Beautiful girl');