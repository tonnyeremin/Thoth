
const api = "https://thothwebapp.azurewebsites.net/api/quoteitem";

$(document).ready(function(){
    $('#loadspinner').hide(); 
    $('#showrandombtn').click(showrandom);
    $('#submitform').submit(submit);
    $('#linktext').val(this.URL);
    populateShare(this);
});

function populateShare(document){

  $url = document.URL
  $text = encodeURIComponent('Check your tips here');

  $('#linktext').val($url);
  $('#tweet').attr("href", 'https://twitter.com/intent/tweet?text=Check%20your%20tips%20at&url='+ encodeURIComponent($url))
  $('#facebook').attr("href",'http://www.facebook.com/sharer/sharer.php?display=page&u='+encodeURIComponent($url)+'&t=Check%20your%20tips%20at')
  $('#linkedin').attr("href", 'https://www.linkedin.com/sharing/share-offsite/?url='+encodeURIComponent($url))
  $('#whatsapp').attr("href", 'https://api.whatsapp.com/send?text=' + $text  + '%20' +encodeURIComponent($url))
  $('#telegram').attr("href", 'https://t.me/share/url?url=' + $url + '&text=zxXZx' +$text)
  $('#envelope').attr("href", 'mailto: ?subject=' + $text + '&body=' + $url)
}

function populateQuoteShare(data){

  $url = encodeURIComponent(document.URL);
  $quote = data.primaryText;

  $auth = data.author;
  if($auth.length>50)
  {
    $auth = $auth.substring(0,50);
    $auth = $auth+"...";
  }

  if( $quote.length>200)
  {
    $quote = $quote.substring(0,200);
    $quote = $quote + "... ";
  }
  $text = encodeURIComponent($quote +' by '+ $auth);

  $('#mtweet').attr("href", 'https://twitter.com/intent/tweet?text='+$text+'&url='+$url)
  $('#mfacebook').attr("href",'http://www.facebook.com/sharer/sharer.php?display=page&u='+$url+'&t=' + $text)
  $('#mlinkedin').attr("href", 'https://www.linkedin.com/sharing/share-offsite/?url='+$url)
  $('#mwhatsapp').attr("href", 'https://api.whatsapp.com/send?text=' + $text  + '%20' +$url)
  $('#mtelegram').attr("href", 'https://t.me/share/url?url=' + $url + '&text=' +$text)
  $('#menvelope').attr("href", 'mailto: ?subject=' + 'TipsForMe' + '&body=' + $text)
}

function showrandom(data){
    $('#showrandombtn').hide();
    $('#loadspinner').show();   
    $.getJSON(api, {
            }).done(function(data){
               
                $('#primarytext').text(data.primaryText)
                $('#secondarytext').text(data.secondaryText)
                $('#author').text(data.author)
                $('#loadspinner').hide();
                populateQuoteShare(data); 
                $('#quoteModal').modal();   
                $('#showrandombtn').show();
            })
          
        };
      
 function submit(e){
     e.preventDefault();
     $('#submittionform').hide();
     $.ajax({
        url: api,
        type: 'post',
        crossdomain: true,
        contentType: 'application/json',
        dataType: 'json',
        processData: false,
        data: JSON.stringify(objectifyForm($('#submitform').serializeArray())),

        success: function(data) {
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
