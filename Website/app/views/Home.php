<?php
include '../models/User.class.php';
include '../core/db.class.php';

$user = new User();
$msg = "";
session_start();

if (!isset($_SESSION['loggedin'])) {
    header('Location: ../../Index.php');
    exit;
}
$dbconn = new DB();
$username = $_SESSION['username'];
$password = $_SESSION['password'];

$query_employee = "SELECT * FROM employee WHERE Username = '$username' AND Password = '$password'";
$employee_statement = $dbconn->connect()->prepare($query_employee);
$employee_statement->execute();
$employees = $employee_statement->fetchAll();
$employee_statement->closeCursor();

foreach ($employees as $empl) {
    $_SESSION['employeeId'] = $empl['EmployeeID'];
}

?>

<!DOCTYPE html>
<html lang="en">

<head>
  <link rel="stylesheet" type="text/css" href="../../public/css/main.css">
  <link rel="stylesheet" type="text/css" href="../../public/css/normalize.css">
  <link rel="stylesheet" type="text/css" href="../../public/css/ionicons-master/docs/css/ionicons.min.css">
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
  <link href="https://fonts.googleapis.com/css?family=Lato:100,300,300i,400&display=swap" rel="stylesheet">
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,user-scalable=no">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <meta name="HandheldFriendly" content="true">
  <title>Media Bazaar</title>
</head>

<body class="body-home-page">

  <header class="header-home" id="home">

    <nav>
      <div class="row">
        <a href="#home"><img alt="logo" class="logo-nav" src="../../public/img/logo2.png"></a>
        <ul class="main-nav">
          <li><a href="#">Home</a></li>
          <li><a href="Schedule.php">Shedule</a></li>
        </ul>

        <div class="main-nav logout">
          <a href="../../public/php/logout.php">Log Out</a>
        </div>
      </div>
    </nav>

  </header>


  <section class="home-page-cover-wallpaper-section">

    <div class="content-block">
      <div class="row">
        <img src="../../public/img/wallpaper.jpg" alt="wallpaper" class="cover-picture">
      </div>
      <?php foreach ($employees as $employee) ?>
      <div class="wallpaper-personal-info">
        <div class="wallpaper-box-content-personal-info">
          <div class="wallpaper-title-box-inside">
            <div class="text-edit-personal-info">
              <!-- Name from the database -->
              <h1><?php
                  echo $employee['FirstName'] . " " . $employee['LastName'];
                  ?></h1>
              <div class="job-position">
                <i class="ion-ios-man"></i>
                <h3><?php echo $employee['Position']; ?></h3> <!-- Position from the database -->
              </div>
              <div class="department">
                <i class="ion-ios-locate"></i>
                <h3><?php
                    if ($employee['Departament'] == NULL) {
                      $message = "Not yet assigned to department";
                      echo $message;
                    } else {
                      echo $employee['Departament'];
                    }
                    ?></h3> <!-- Department from the database -->
              </div>

              <div class="started-date">
                <i class="ion-ios-calendar"></i>
                <h3><?php echo "Borned on: " . $employee['DateOfBirth']; ?></h3> <!-- Born date from the database -->
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="container-personal-picture">
        <div class="container-profile-pic">
          <div class="profile-picture">

          </div>
          <i class="ion-ios-build icon-customize customize-profile-picture"></i>
        </div>

        <div class="content-below-profile-picture">
          <?php foreach ($employees as $employee) ?>
          <h2 class="work-shift-title">Upcoming work shifts</h2>
          <ul>
            <?php
            $employee_id = $employee['EmployeeID'];
            #the shifts will be taken from the database using the employee id
            $string = "Assigned";
            $query_shift = "SELECT * FROM schedule WHERE EmployeeID = '$employee_id' AND Status = '$string'";
            $shift_statement = $dbconn->connect()->prepare($query_shift);
            $shift_statement->execute();
            $shifts = $shift_statement->fetchAll();
            $shift_statement->closeCursor();

            //list only the upcoming shifts
            foreach ($shifts as $employee_shifts) {
              //get date of today in the same format as the dates are stored in the db
              $today_date = date('d/m/Y');
              $today_date = explode('/', $today_date);
              $date_shift = explode('/', $employee_shifts['Date']);

              if ((($date_shift[2] == $today_date[2]) && ($date_shift[1] == $today_date[1])) && ((int) $date_shift[0] >= (int) $today_date[0])) {
                echo "<li><h4 class=\"days-work-shifts\">" . $employee_shifts['Date'] . " -> " . $employee_shifts['Shift'] . "</h4></li>";
              } else {
                if (((int) $date_shift[1] != $today_date[1]) && ((int) $date_shift[1] >= (int) $today_date[1]) && ($date_shift[2] == $today_date[2]) && (((int) $date_shift[0] >= (int) $today_date[0]) || (((int) $date_shift[0] < (int) $today_date[0])))) {
                  echo "<li><h4 class=\"days-work-shifts\">" . $employee_shifts['Date'] . " -> " . $employee_shifts['Shift'] . "</h4></li>";
                } else {
                  if (((int) $date_shift[2] != (int) $today_date[2]) &&  ((int) $date_shift[2] >= (int) $today_date[2]) &&  (((int) $date_shift[1] >= (int) $today_date[1]) || ((int) $date_shift[1] < (int) $today_date[1])) && (((int) $date_shift[0] >= (int) $today_date[0])  || ((int) $date_shift[0] < (int) $today_date[0]))) {
                    echo "<li><h4 class=\"days-work-shifts\">" . $employee_shifts['Date'] . " -> " . $employee_shifts['Shift'] . "</h4></li>";
                  }
                }
              }
            }
            ?>
          </ul>
        </div>

      </div>

    </div>
  </section>


  <!-- CONTACT SECTION-->
  <section class="home-page-section">
    <div class="row">
      <h2 class="home-content">Contact</h2>

      <div class="email-section">
        <div class="email">
          <i class="ion-ios-mail"></i>
          <h3 class="header3-profilePage">Email</h3>
        </div>
        <div class="email-update" name="email">
          <p id="mail-written"><?php echo $employee['Email']; ?></p>
        </div>
        <div class="customize-home-section">
          <i id="custom-contact-email" class="ion-ios-build  icon-customize icon-customize-contact-section"></i>
        </div>
      </div>


      <div class="phone-section">
        <div class="phone-number">
          <i class="ion-ios-call"></i>
          <h3 class="header3-profilePage">Mobile</h3>
        </div>
        <div class="phone-update" name="email">
          <p id="phone-nr"><?php echo $employee['PhoneNumber']; ?></p>
        </div>
        <div class="customize-home-section">
          <i id="custom-contact-phone" class="ion-ios-build  icon-customize icon-customize-contact-section"></i>
        </div>
      </div>


    </div>
  </section>



  <!-- ACCOUNT CREDENTIALS SECTION-->
  <section class="home-page-section">
    <div class="row">
      <h2 class="home-content">Account Credentials</h2>

      <div class="username-section">
        <div class="username-homePage">
          <i class="ion-ios-person"></i>
          <h3 class="header3-profilePage">Username</h3>
        </div>
        <p id="username"><?php echo $employee['Username']; ?></p>

      </div>

      <div id="password-reference" class="password-section">
        <div class="password-homePage">
          <i class="ion-ios-key"></i>
          <h3 class="header3-profilePage">Password</h3>
        </div>
        <div class="update-password update-fields">
          <input class="input-pass" type="password" id="original-input-pass" value="<?php echo $employee['Password']; ?>" disabled>
          <input type="checkbox" id="check" name="check-pass" value="Show password"><label id="label-show-pass" for="check">Show password</label>
          <div id="feedback"></div>
        </div>


        <div class="customize-home-section">
          <i id="custom-password" class="ion-ios-build  icon-customize icon-customize-contact-section"></i>
        </div>
      </div>

    </div>
  </section>



  <!-- ABOUT SECTION-->
  <section class="home-page-section">
    <div class="row">
      <h2 class=" home-content">About</h2>

      <div class="bio-section">
        <div class="biography">
          <i class="ion-ios-book"></i>
          <h3>Bio</h3>
        </div>
        <p id="personal-bio"><?php echo $employee['PersonalInfo']; ?></p>

        <div class="customize-home-section">
          <i id="custom-bio" class="ion-ios-build  icon-customize icon-customize-bio-section"></i>
        </div>
      </div>

    </div>
  </section>

  <!-- <aside class="send-email"> -->

  <section class="home-page-content">
    <div class="row">
      <h2 class="home-content">Make an inquiry</h2>
      <form class="formInquiry" action="<?php $user->SendInquiry()?>" method="POST">
        <input name="subject" placeholder="Subject..."><br>
        <textarea cols="30" rows="10" id="emailContent" name="emailContent" required></textarea><br>
        <input class="submit" type="submit" name="Submit" value="Send">
        <?php echo "$msg"; ?>
      </form>
    </div>
  </section>


  <!-- </aside> -->






  <footer>
    <div class="row">

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

    <div class="row">
      <p id="footer-p">
        Copyright &copy; 2020 by EasySoft. All rights reserved.
      </p>
    </div>
  </footer>
  <script src="../../public/js/updateProfile.js"></script>
</body>

</html>