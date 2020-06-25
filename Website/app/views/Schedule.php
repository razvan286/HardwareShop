<?php

//ranim PART
session_start();
include '../models/User.class.php';
include '../core/db.class.php';
include '../models/Calendar.class.php';

$calendar = new Calendar();


?>


<!DOCTYPE html>
<html lang="en">
<head>
	<link rel="stylesheet" type="text/css" href="../../public/css/main.css">
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://fonts.googleapis.com/css?family=Lato:100,300,300i,400&display=swap" rel="stylesheet">
	<link rel="stylesheet" type="text/css" href="../../public/css/ionicons-master/docs/css/ionicons.min.css">
  	<meta charset="UTF-8">
  	<meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,user-scalable=no">
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  	<meta http-equiv="X-UA-Compatible" content="ie=edge">
  	<meta name="HandheldFriendly" content="true">
	 <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
	<title>Calendar</title>

	<style>
		table {
			table-layout: fixed;
		}

		td {
			width: 20;	
		}

		.today {
			background: green;
		}
	</style>
</head>

<body class="body-calendar-page">

	<header class="header-home" id="home">
      <nav>
        <div class="nav-calendar">
          <a href="#home"><img alt="logo" class="logo-nav" src="../../public/img/logo2.png"></a>
          <ul class="main-nav calendar-nav">
              <li><a href="Home.php">Home</a></li>
              <li><a href="#">Shedule</a></li>
          </ul>
          <div class="main-nav logout logout-calendar">
            <a href="../../public/php/logout.php">Log Out</a>
          </div>
        </div>
      </nav>

    </header>
    
	<div class = "container calendar-container">
		<div class = "row">
			<div class = "col-md-12">
				<?php 
					$dateComponents = getdate();
					if (isset($_GET['month']) && isset($_GET['year'])){
						# code...
						$month = $_GET['month'];
						$year = $_GET['year'];
					}
					else{
						$month = $dateComponents['mon'];
						$year = $dateComponents['year'];	
					}
					$calendar->build_calendar($month, $year);
					
				?>
			</div>
		</div>
	</div>

	<footer class="footer-calendar">
      <div class="row-footer">

        <div class="col span-1-of-2">
          <ul class="footer-nav">
            <li><a href="#">About Us</a></li>
            <li><a href="#">Blog</a></li>
            <li><a href="#">Services</a></li>
          </ul>
        </div>

        <div class="col span-1-of-2">
          <ul class="social-links">
            <li><a href="#"><i class="ion-logo-facebook"></i> </a> </li>
            <li><a href="#"><i class="ion-logo-instagram"></i> </a></li>
            <li><a href="#"><i class="ion-logo-twitter"></i></a></li>
          </ul>
        </div>
      </div>

      <div class="row-footer">
        <p id="footer-p">
          Copyright &copy; 2020 by EasySoft. All rights reserved.
        </p>
      </div>
    </footer>

</body>
</html>