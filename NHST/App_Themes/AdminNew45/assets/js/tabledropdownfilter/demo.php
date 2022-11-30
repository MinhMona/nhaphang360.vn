<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8"/>
    <title>Table Dropdown Filter</title>
    <link href="w3.css" rel="stylesheet"/>
    <style>
        .container{width:960px;margin:30px auto;}
        thead select{border: 1px solid #ffffff;width:100%;}
    </style>
</head>
<body>

    <div class="container">
        <table id="mytable" class="w3-table-all">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Gender</th>
                    <th>Address</th>
                </tr>
            </thead>
            <tbody>
                <?php
                $db = new PDO("mysql:host=localhost;dbname=mydata","root","");
                $stmt = $db->prepare("select * from addmore");
                $stmt->execute();
                while($row = $stmt->fetch()){
                ?>
                <tr>
                    <td><?php echo $row['user_id'] ?></td>
                    <td><?php echo $row['user_name'] ?></td>
                    <td><?php echo $row['user_gender'] ?></td>
                    <td><?php echo $row['user_address'] ?></td>
                </tr>
                <?php
                }
                ?>
            </tbody>
        </table>
    </div>

    <script src="jquery.min.js"></script>
    <script src="ddtf.js"></script>
    <script>
        $('#mytable').ddTableFilter();
    </script>
</body>
</html>