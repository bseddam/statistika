var form = $('.music-form');
var error3 = $('.alert-danger', form);
var success3 = $('.alert-success', form);



form.validate({
    errorElement: 'span', //default input error message container
    errorClass: 'help-block help-block-error', // default input error message class
    focusInvalid: true, // do not focus the last invalid input
    ignore: "", // validate all fields including form hidden input
    rules: {   
        //categoryID: {
        //    required: true
        //},
        //moodID: {
        //    required: true
        //},
        //langID: {
        //    required: true
        //},
        //title: {
        //    required: true
        //},
        //desc: {
        //    required: true
        //},
        //url: {
        //    required: true
        //},
        //type: {
        //    required: true
        //},
        soundfile: {
            required: true
        }
    },

    messages: {
        // custom messages for radio buttons and checkboxes
        //membership: {
        //    required: "Please select a Membership type"
        //},
        //desc: {
        //    required: "Please add desc"
        //},
        //service: {
        //    required: "Please select  at least 2 types of Service",
        //    minlength: jQuery.validator.format("Please select  at least {0} types of Service")
        //}
    },

    errorPlacement: function (error, element) { // render error placement for each input type
        if (element.parent(".input-group").size() > 0) {
            error.insertAfter(element.parent(".input-group"));
        } else if (element.attr("data-error-container")) {
            error.appendTo(element.attr("data-error-container"));
        } else if (element.parents('.radio-list').size() > 0) {
            error.appendTo(element.parents('.radio-list').attr("data-error-container"));
        } else if (element.parents('.radio-inline').size() > 0) {
            error.appendTo(element.parents('.radio-inline').attr("data-error-container"));
        } else if (element.parents('.checkbox-list').size() > 0) {
            error.appendTo(element.parents('.checkbox-list').attr("data-error-container"));
        } else if (element.parents('.checkbox-inline').size() > 0) {
            error.appendTo(element.parents('.checkbox-inline').attr("data-error-container"));
        } else {
            error.insertAfter(element); // for other inputs, just perform default behavior
        }
    },

    invalidHandler: function (event, validator) { //display error alert on form submit   
        success3.hide();
        error3.show();
        Metronic.scrollTo(error3, -200);
    },

    highlight: function (element) { // hightlight error inputs
        $(element)
             .closest('.form-group').addClass('has-error'); // set error class to the control group
    },

    unhighlight: function (element) { // revert the change done by hightlight
        $(element)
            .closest('.form-group').removeClass('has-error'); // set error class to the control group
    },

    success: function (label) {
        label
            .closest('.form-group').removeClass('has-error'); // set success class to the control group
    },

    submitHandler: function (form) {
        alert(1);
        var options = {
            target: '.aaaaaaa',   // target element(s) to be updated with server response 
            beforeSubmit: alert(1),  // pre-submit callback 
            success: showResponse,  // post-submit callback 

            // other available options: 
            url: '/admin/music/add',       // override for form's 'action' attribute 
            type: 'post',      // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json'        // 'xml', 'script', or 'json' (expected server response type) 
            //clearForm: true        // clear all form fields after successful submit 
            //resetForm: true        // reset the form after successful submit 

            // $.ajax options can be used here too, for example: 
            //timeout:   3000 
        };

        // bind form using 'ajaxForm' 
        $('.music-form').ajaxForm(options);


      success3.show();
        error3.hide();

    }

});

//apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
$('.select2me', form).change(function () {
    form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
});

