<?php
        if (isset($_SESSION['username'])) {
            session_destroy();
            //header('Location: ../../Index.php');
            echo "<script>location.href='../../Index.php'</script>";
        } else {
            //header('Location: ../../Index.php');
            echo "<script>location.href='../../Index.php'</script>";
        }
