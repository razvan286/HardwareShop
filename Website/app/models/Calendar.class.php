<?php

class Calendar
{
    public function GetSelectedDate($Employee, $date){

        try{
            $dbconn = new DB();
            $query = "SELECT Status FROM schedule WHERE `Date` = '$date' AND `EmployeeID` = $Employee";
            $stmt = $dbconn->connect()->prepare($query);
    
            $stmt->execute();
            $result = $stmt->fetch();
    
            return $result;
        }
        catch(PDOException $e){
            echo "Error";
        }
    }
    
    public function build_calendar($month, $year){
        $EmployeeID = $_SESSION['employeeId'];
    
        //names of days
        $daysOfWeek = array ('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
    
        //get first day of the month
        $firstDayOfMonth = mktime(0,0,0,$month, 1,$year);
    
        //get number of days for each month
        $numberDays = date('t', $firstDayOfMonth);
    
        //get info about day
        $dateComponents = getdate($firstDayOfMonth);
    
        //get name of month
        $monthName = $dateComponents['month'];
    
        //get index value 0-6
        $dayOfWeek = $dateComponents['wday'];
    
        //get current date
        //ranim part
        $dateToday = date('Y-m-d');
        //ranim PART
        //new
        //$dateToday = date('d/m/Y');
        //creating html table
        $calendar = "<table class = 'table table-bordered'>";
        $calendar.= "<center><h2>$monthName $year</h2>";   
        $calendar.= "<a class='btn btn-xs btn-primary' href = '?month=".date('m', mktime(0,0,0, $month-1,1,$year))."&year=".date('Y', mktime(0,0,0, $month-1,1,$year))."'>Previous Month</a>";   
        $calendar.= "<a class='btn btn-xs btn-primary' href = '?month=".date('m')."&year=".date('Y')."'>Current Month</a>";
        $calendar.= "<a class='btn btn-xs btn-primary' href = '?month=".date('m', mktime(0,0,0, $month+1,1,$year))."&year=".date('Y', mktime(0,0,0, $month+1,1,$year))."'>Next Month</a></center><br>";
    
        /////
    
        $calendar.= "<tr>";
        //creating calendar headers
        foreach ($daysOfWeek as $day) {
            # code...
            $calendar.= "<th class='header'>$day </th>";
        }
        $calendar.= "</tr><tr>";
        //only 7 columns
        if ($dayOfWeek > 0) {
            # code...
            for ($i=0; $i < $dayOfWeek; $i++) { 
                # code...
                $calendar.= "<td></td>";
            }
        }
        //initialize day counter
        $currentDay = 1;
        //get month number
        $month = str_pad($month, 2, "0", STR_PAD_LEFT);
        while ($currentDay <= $numberDays) {
            //if seventh col (saturday) reached start new row
            if ($dayOfWeek == 7) {
                # code...
                $dayOfWeek = 0;
                $calendar.= "</tr><tr>";
            }
            # code...
            $currentDayRel = str_pad($currentDay, 2, "0", STR_PAD_LEFT);
            $date = "$year-$month-$currentDayRel";
            $formatDate = date('d/m/Y', strtotime(($date)));
            //new
            //$date = "$currentDayRel/$month/$year";   
            $dayname = strtolower(date('l', strtotime($date)));
            $evenNum = 0;
            //ranim PART
            $today = $date ==date('Y-m-d')?"today":"";
            //new
            //$today = $date ==date('d/m/Y')?"today":"";
            $result = $this->GetSelectedDate($EmployeeID, $formatDate);
            if ($date < date('Y-m-d')) {
                # code...
                $calendar.= "<td><h4>$currentDay</h4> <button class='btn btn-danger btn-xs'>N/A</button>";
            }
            else {
                if ($result != null) {
                    # code...
                    if ($result[0]== 'Selected') { //show selected days
                    # code...
                        $calendar.= "<td class ='$today'><h4>$currentDay</h4> <a href='CancelDay.php?date=".$date."' class='btn btn-warning btn-xs'>Selected</a>";
                        // $calendar.= "<a href='select.php?date=".$formatDate."' class='btn btn-success btn-xs'>Select Another Shift</a>";
                    }
                    else if ($result[0]== 'Cancelled') { //show cancelled days
                    # code...
                        $calendar.= "<td><h4>$currentDay</h4> <button class='btn btn-danger btn-xs'>Cancelled</button>";
                    }
                    else if ($result[0]== 'Assigned') { //show assigned days
                    # code...
                        $calendar.= "<td class ='$today'><h4>$currentDay</h4> <a href='CancelDay.php?date=".$date."' class='btn btn-warning btn-xs'>Assigned</a>";
                    }
                }
                else { // show unselected/normal days
                    $calendar.= "<td class ='$today'><h4>$currentDay</h4> <a href='Select.php?date=".$date."' class='btn btn-success btn-xs'>Select</a>";
                }
            }
            $calendar.= "</td>";
            //increase counters
            $currentDay++;
            $dayOfWeek++;
        }
    
        //complete the row of last week month, 
        if ($dayOfWeek != 7){
            $remainingDays = 7-$dayOfWeek;
    
            for ($i=0; $i < $remainingDays ; $i++) { 
                # code...
                $calendar.= "<td></td>";
            }
        }
        $calendar.= "</tr>";
        $calendar.= "</table>";
    
        echo $calendar;
    
    }

    public function timeslots($day)
    {
        //showing shifts
        $slots = array();
        if ($day == "Sunday") { //sunday shift
            $slots[0] = "12:00-18:00";
        } else {
            if ($day == "Saturday") { //saturday shifts
                $slots[0] = "09:00-15:00";
                $slots[1] = "15:00-18:00";
            } else { // other days shifts
                $slots[0] = "07:00-12:00";
                $slots[1] = "12:00-17:00";
                $slots[2] = "17:00-22:00";
            }
        }
        return $slots;
    }
    
}