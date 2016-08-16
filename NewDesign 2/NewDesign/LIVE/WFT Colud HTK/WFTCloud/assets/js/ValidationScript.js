
//**********************************************************

$(document).ready(function () {
    KeyDownValidation();
});

//**********************************************************
function KeyDownValidation() {
    //**********************************************************
    $(".Alphabet").keydown(function (e) {
        //$(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if ((e.shiftKey == true && e.keyCode == 16) || e.ctrlKey == true) {
            return true;
        }
        if ((e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 32 || e.keyCode == 35 || e.keyCode == 36 ||
                    e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 45 || e.keyCode == 46 || (e.shiftKey == true && (e.keyCode >= 65 && e.keyCode <= 90))) {
           // $(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
           // $(this).attr('style', 'border-color: #FF0000');
            $('#error_label').html("Enter only alphabet values");
            return false;
        }
    });
    //**********************************************************
    $(".Numeric").keydown(function (e) {
        $('#txtAlphaNumeric').val(e.keyCode);
      //  $(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if (e.shiftKey == true) {
            if (e.shiftKey == true && e.keyCode != 16) {
                //$(this).attr('style', 'border-color: #FF0000');
                $('#error_label').html("Enter only numeric values");
                return false;
            }
            return false;
        }
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 35 || e.keyCode == 36 ||
                     e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 144 || e.ctrlKey == true) {
            //$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
            //$(this).attr('style', 'border-color: #FF0000');
            $('#error_label').html("Enter only numeric values");
            return false;
        }
    });
    //**********************************************************
    $(".Numeric1").keydown(function (e) {
        $('#txtAlphaNumeric').val(e.keyCode);
        ////$(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if (e.shiftKey == true) {
            if (e.shiftKey == true && e.keyCode != 16) {
                ////$(this).attr('style', 'border-color: #FF0000');
                $('#error_label').html("Enter only numeric values");
                return false;
            }
            return false;
        }
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 35 || e.keyCode == 36 ||
                     e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 144 || e.ctrlKey == true) {
            ////$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
            ////$(this).attr('style', 'border-color: #FF0000');
            $('#error_label').html("Enter only numeric values");
            return false;
        }
    });

    //**********************************************************
    $(".Numeric1").focusout(function (e) {
        var val = this.value;
        if (val == "" || val == "0") {
            this.value = "1";
        }
        var s = parseInt(this.value);
        if (s == 0) {
            this.value = "1";
        }
                
    });

    //**********************************************************
    $(".AlphaNumeric").keydown(function (e) {
        //$(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if ((e.shiftKey == true && e.keyCode == 16) || (e.shiftKey == true && (e.keyCode >= 65 && e.keyCode <= 90))) {
            //$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        if ((e.shiftKey != true && ((e.keyCode >= 48 && e.keyCode <= 57)) || (e.keyCode >= 96 && e.keyCode <= 105) || (e.keyCode >= 65 && e.keyCode <= 90) ||
                     e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 32 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 39 ||
                     e.keyCode == 45 || e.keyCode == 46 || e.ctrlKey == true)) {
            //$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
            $('#error_label').html("Enter only alpha or numeric");
            //$(this).attr('style', 'border-color: #FF0000');
            return false;
        }
    });
    //**********************************************************
    $(".Money").keydown(function (e) {
        $('#txtAlphaNumeric').val(e.keyCode);
 //     $(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if (e.shiftKey == true) {
            if (e.shiftKey == true && e.keyCode != 16) {
 //            $(this).attr('style', 'border-color: #FF0000');
                $('#error_label').html("Enter only numeric values");
                return false;
            }
            return false;
        }
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 35 || e.keyCode == 36 ||
                 e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 110 || e.keyCode == 144 || e.keyCode == 190 || e.ctrlKey == true) {
//        $(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
//         $(this).attr('style', 'border-color: #FF0000');
            $('#error_label').html("Enter only numeric values");
            return false;
        }
    });
    //**********************************************************    
    //**********************************************************
    $(".Money").keyup(function (e) {
        var val = this.value;
        var re = /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9]+)$/g;
        var re1 = /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9]+)/g;
        if (re.test(val)) {

        } else {
            val = re1.exec(val);
            if (val) {
                this.value = val[0];
            } else {
                this.value = "";
            }
        }
    });
    //**********************************************************
    $(".AlphabetSpaceDot").keydown(function (e) {
        //$(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if ((e.shiftKey == true && e.keyCode == 16) || e.ctrlKey == true) {
            return true;
        }
        if ((e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 32 || e.keyCode == 35 || e.keyCode == 36 ||
        e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 110 || e.keyCode == 190 ||
         (e.shiftKey == true && (e.keyCode >= 65 && e.keyCode <= 90))) {
            //$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
            //$(this).attr('style', 'border-color: #FF0000');
            $('#error_label').html("Enter only alphabet values");
            return false;
        }
    });
    //**********************************************************
    $(".AlphaNumericSpaceDot").keydown(function (e) {
        //$(this).attr('style', 'border-color: none');
        $('#error_label').html("");
        if ((e.shiftKey == true && e.keyCode == 16) || (e.shiftKey == true && (e.keyCode >= 65 && e.keyCode <= 90))) {
            //$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        if ((e.shiftKey != true && ((e.keyCode >= 48 && e.keyCode <= 57)) || (e.keyCode >= 96 && e.keyCode <= 105) || (e.keyCode >= 65 && e.keyCode <= 90) ||
                     e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 32 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 39 ||
                     e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 110 || e.keyCode == 190 || e.ctrlKey == true)) {
            //$(this).attr('style', 'border-color: none');
            $('#error_label').html("");
            return true;
        }
        else {
            $('#error_label').html("Enter only alpha or numeric");
            //$(this).attr('style', 'border-color: #FF0000');
            return false;
        }
    });
    //**********************************************************
    //**********************************************************
    $(".TimeField1").keydown(function (e) {
        $('#txtAlphaNumeric').val(e.keyCode);
        alert(e.keyCode);
        if ((e.keyCode >= 48 && e.keyCode <= 57) || ((e.shiftKey == true && e.keyCode == 58))) {
            $('#error_label').html("");
            return true;
        }
        else {
            return false;
        }
    });
    //**********************************************************
}
//**********************************************************