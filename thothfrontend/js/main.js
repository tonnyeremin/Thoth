
const api = "https://thothwebapp.azurewebsites.net/api/quotaitem";
$(document).ready(function(){
$('#showrandombtn').click(showrandom)
});

function showrandom(data){
    $.getJSON(api, {
        format:"json"
            }).done(function(data){  
                $('#primarytext').text(data.primaryText)
                $('#secondarytext').text(data.secondaryText)
                $('#author').text(data.Author)
                $('#quoteModal').modal()   
            })
        };
      
