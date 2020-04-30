
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
        dataType: 'application/json',
        data: $("#submitform").serialize(),
        success: function(data) {
          
        }
});
 }     
