
const api = "https://thothwebapp.azurewebsites.net/api/quoteitem";
$(document).ready(function(){
    $('#showrandombtn').click(showrandom)
    $('#submitform').submit(submit)
});

function showrandom(data){
    $.getJSON(api, {
            }).done(function(data){  
                $('#primarytext').text(data.primaryText)
                $('#secondarytext').text(data.secondaryText)
                $('#author').text(data.Author)
                $('#quoteModal').modal()   
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
          console.log(data);
          
        }
});

 }  
 
 function objectifyForm(formArray) {//serialize data function

    var returnArray = {};
    for (var i = 0; i < formArray.length; i++){
      returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
  };
