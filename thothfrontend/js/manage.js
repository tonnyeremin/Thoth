const api = "https://thothwebapp.azurewebsites.net/manage/quoteitem";

//const api = "https://localhost:5001/manage/quoteitem";

$(document).ready(function(){
    $(".shownew").click(shownew);
    $(".showall").click(showall);
    $(".addrow").click(addnewrow);
    $(".butomSave").click(savechanges);
    $(".prev a").click(function(){switchpage(true)});
    $(".next a").click(function(){switchpage(false)});
    refreshview(true);                            
    });

    function shownew()
    {
        $("#recordsTable").data("new", true)
        $(".current a").data("current", 1)
        $(".showall").removeClass('active');
        $(".shownew").addClass('active');
        refreshview();
    };

    function showall()
    {
        $("#recordsTable").data("new", false)
        $(".current a").data("current", 1)
        $(".shownew").removeClass('active');
        $(".showall").addClass('active');
        refreshview();
    }

    function addnewrow(){
        $("#id").prop( "disabled", true );
        $("#postTime").prop( "disabled", true );
        $('#quoteModal').modal();   
    };

    function savechanges()
        {
            var $id = $("#quoteModal").data("id"); 
            if($id=="")
            {
                $.ajax({
                    url: api,
                    type: 'post',
                    crossdomain: true,
                    contentType: 'application/json',
                    dataType: 'json',
                    processData: false,
                    data: JSON.stringify(objectifyForm($('#editform').serializeArray())),
                    success: function(data) {
                        $('#quoteModal').modal('hide');
                            refreshview()
                        },
                        done: function()
                        {
                            $('#quoteModal').modal('hide');   
                        }
                        });
            }
            else
            {
                $.ajax({
                    url: api+"/"+$id,
                    type: 'put',
                    crossdomain: true,
                    contentType: 'application/json',
                    dataType: 'json',
                    processData: false,
                    data: JSON.stringify(objectifyForm($("#editform").serializeArray())),
                    success: function(data) {
                        $('#quoteModal').modal('hide');
                            refreshview();
                        },
                        done: function()
                        {
                            $('#quoteModal').modal('hide');   
                        }
                        });
            }
        };

    function refreshview(){
        $new = $("#recordsTable").data("new");
        $current =  $(".current a").data("current");
        $link = api+"?/NewOnly="+$new+"&pageNumber="+ $current;
        
        $("#recordsTable tr").slice(1).remove();
        $.getJSON($link, {
      
        }).done(function(data, text, request){
               
            populatepagination(request.getResponseHeader('x-pagination'));    
            
            data.forEach(record => addrow(record));
            }) 
    };

    function populatepagination(header)
    {
        var headerObj = JSON.parse(header);

        if(headerObj.HasPrevious){
            $(".prev").removeClass("disabled");
        }
        else{
            $(".prev").addClass("disabled");
        }

        $(".current a span").html(headerObj.CurrentPage + " from " + headerObj.TotalPages)
        $(".current a").data("current", headerObj.CurrentPage)

        if(headerObj.HasNext){
            $(".next").removeClass("disabled");
        }
        else{
            $(".next").addClass("disabled");
        }
    };

    function switchpage(back)
    {
        $current =  $(".current a").data("current")
        if(back){
            $current--;
        }
        else{
            $current++;
        }
        $(".current a").data("current", $current);
        refreshview();

    };

    function editrow(obj){
        var $id = $(obj).data("id"); 
        $.getJSON(api+"/"+$id, {
        }).done(function(data){
            $("#id").val(data.id);
            $('#quoteModal').data("id", data.id);  
            $("#primaryText").val(data.primaryText);
            $("#secondaryText").val(data.secondaryText);
            $("#author").val(data.author);
            $("#postTime").val(data.postTime);
            $("#isVisible").attr('checked',data.isVisible);
            $('#quoteModal').modal();   
        }) 
    };

    function deleterow(obj)
    {
        var $id = $(obj).data("id");
        $.ajax({
            url: api+"/"+$id,
            type: 'delete',
            crossdomain: true,
            success: function(data) {
                refreshview();
                alert("Delete sucefully!")
            },
    }); 
    };


    function alert(message){
        $('#alert').html('<div class="alert alert-secondary"><a class="close" data-dismiss="alert">×</a><span>'+message+'</span></div>');
        $('.alert').alert()
    };

    function addrow(data){
        var row = '<tr id= "'+ data.id +'"><td scope="row">' + data.id + '</td>' +
                    '<td>' + data.primaryText + '</td>' +
                        '<td>' + data.secondaryText + '</td>' +
                            '<td>' + data.author + '</td>' +
                                '<td>' + data.postTime + '</td>' +
                                    '<td>' + data.isVisible + '</td>' +
                                        '<td> <button type="button" onclick="editrow(this)" data-id ="' + data.id +  '"class="btn btn-outline-primary editrow">Edit</button></td>' +
                                        '<td> <button type="button" onclick="deleterow(this)" data-id ="' + data.id +  '"class="btn btn-outline-danger deleterow">Delete</button></td>' +  
                                  '</tr>';
            $('#recordsTable tr:last').after(row); 
                                        
    };

    function objectifyForm(formArray) {
        var returnArray = {};
        for (var i = 0; i < formArray.length; i++)
        {
           if(!formArray[i].disabled)
                returnArray[formArray[i]['name']] = formArray[i]['value'];
        }
        return returnArray;
    };








