<?php
$adminemail = "info@rafalborowski.com";

if ($_GET['send'] == 'newsletter')
{					
	$_uemail = $_POST['user-email'];
						
	$email_check = '';
	$return_arr = array();

	if($_uemail=="")
	{
		$return_arr["frm_check"] = 'error';
		$return_arr["msg"] = "Please enter your email address.";
	} 
	else if(filter_var($_uemail, FILTER_VALIDATE_EMAIL)) 
	{
	
	$to = $adminemail;
	$from = $_uemail;
	$subject = "New newsletter member from WFTCloud";
	$message = "Member email: " .$_uemail;

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