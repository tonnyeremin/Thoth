
const api = "https://thothwebapp.azurewebsites.net/api/quoteitem";
$(document).ready(function(){
$('#showrandombtn').click(showrandom)
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
      
