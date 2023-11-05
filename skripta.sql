-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema biljeske
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema biljeske
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `biljeske` DEFAULT CHARACTER SET utf8 ;
USE `biljeske` ;

-- -----------------------------------------------------
-- Table `biljeske`.`tip`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biljeske`.`tip` (
  `idTip` INT NOT NULL AUTO_INCREMENT,
  `opis` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idTip`),
  UNIQUE INDEX `opis_UNIQUE` (`opis` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `biljeske`.`biljeska`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biljeske`.`biljeska` (
  `idBiljeska` INT NOT NULL AUTO_INCREMENT,
  `naslov` VARCHAR(120) NOT NULL,
  `sadrzaj` LONGTEXT NOT NULL,
  `vrijemeKreiranja` DATETIME NOT NULL,
  `tip` INT NOT NULL,
  `daLiJeIzbrisano` TINYINT(1) NULL DEFAULT '0',
  PRIMARY KEY (`idBiljeska`),
  UNIQUE INDEX `naslov` (`naslov` ASC, `vrijemeKreiranja` ASC) VISIBLE,
  INDEX `fk_biljeska_tip_idx` (`tip` ASC) VISIBLE,
  CONSTRAINT `fk_biljeska_tip`
    FOREIGN KEY (`tip`)
    REFERENCES `biljeske`.`tip` (`idTip`))
ENGINE = InnoDB
AUTO_INCREMENT = 31
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
