<?php
$adminemail = "info@rafalborowski.com";

if ($_GET['send'] == 'contact')
{					
	$_uname = $_POST['user-name'];
	$_uemail = $_POST['user-email'];
	$_subject = $_POST['user-subject'];
	$_message =	$_POST['user-message'];
						
	$email_check = '';
	$return_arr = array();

	if($_uname=="" || $_uemail=="" || $_message=="" || $_subject=="")
	{
		$return_arr["frm_check"] = 'error';
		$return_arr["msg"] = "Please fill in all fields!";
	} 
	else if(filter_var($_uemail, FILTER_VALIDATE_EMAIL)) 
	{
	
	$to = $adminemail;
	$from = $_uemail;
	$subject = $_subject;
	$message = "Message from " .$_uname. "email: " .$_uemail. "Message: " .$_message;

	$headers = "MIME-Version: 1.0\r\n";
	$headers .= "Content-type: text/html; charset=iso-8859-1\r\n";
	$headers .= "Content-Transfer-Encoding: 7bit\r\n";
	$headers .= "From: " . $from . "\r\n";

	@mail($to, $subject, $message, $headers);	
	
	} else {
   
	$return_arr["frm_check"] = 'error';
	$return_arr["msg"] = "Please enter a valid email address!";
}

echo json_encode($return_arr);
}

?>