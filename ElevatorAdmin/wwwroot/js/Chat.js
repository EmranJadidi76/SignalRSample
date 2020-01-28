
var myConnectionId = "";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatManageHub").build();

connection.on("ReciveMessage", function (message, ConnectionId,User,ProfilePic) {
    debugger;
    $('[name="Message"]').val('');
    if (myConnectionId === ConnectionId) {
        $.get("/Chat/MyPartial",{message:message,User:User,ProfilePic:ProfilePic},function(result){
            $('#MessagePanel').append(result);
        })

    }
    else {
            $.get("/Chat/RecivePartial",{message:message,User,ProfilePic},function(result){
                 $('#MessagePanel').append(result);
             })
    }
})

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("SetConnection", function (connectionId) {
    myConnectionId = connectionId;
    console.log(myConnectionId);
})

$('#send').on('click', function (e) {
debugger;
    var message = $('[name="Message"]').val();
    var userId = $('#UserId').val();
    connection.invoke("SendMessageToAll", message, userId).catch(function (err) {
        return console.error(err.toString());
    });
    e.preventDefault();
})