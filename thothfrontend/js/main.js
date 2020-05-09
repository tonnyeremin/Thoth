
const api = "https://thothwebapp.azurewebsites.net/api/quoteitem";

$(document).ready(function(){
    $('#loadspinner').hide(); 
    $('#showrandombtn').click(showrandom);
    $('#submitform').submit(submit);
    $('#linktext').val(this.URL);
});

function showrandom(data){
    $('#showrandombtn').hide();
    $('#loadspinner').show();   
    $.getJSON(api, {
            }).done(function(data){
               
                $('#primarytext').text(data.primaryText)
                $('#secondarytext').text(data.secondaryText)
                $('#author').text(data.author)
                $('#loadspinner').hide(); 
                $('#quoteModal').modal()   
                $('#showrandombtn').show();
            })
          
        };
      
 function submit(e){
     e.preventDefault();
     $.ajax({
        url: api,
        type: 'post',
        crossdomain: true,
        contentType: 'application/json',
        dataType: 'json',
        processData: false,
        data: JSON.stringify(objectifyForm($('#submitform').serializeArray())),
        success: function(data) {
          $('#submittionform').hide();
          $('#sumitionresult').show();
          $('#formheader').html("Thank you for submitting your answers.");
        }
});

 }  

 function copylink(){
    var copyText = $('#linktext');
    copyText.focus();
    copyText.select();
    document.execCommand("copy");
 }
 
 function objectifyForm(formArray) {
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++){
      returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
  };
