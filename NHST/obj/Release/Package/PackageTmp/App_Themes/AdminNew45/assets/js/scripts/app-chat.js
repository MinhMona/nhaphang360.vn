$(document).ready(function () {
   "use strict";

   // Sidenav
   if ($(window).width() > 900) {
      $("#chat-sidenav").removeClass("sidenav");
   }

   // Pefectscrollbar for sidebar and chat area
   if ($(".sidebar-chat").length > 0) {
      var ps_sidebar_chat = new PerfectScrollbar(".sidebar-chat", {
         theme: "dark"
      });
   }

   if ($(".chat-area").length > 0) {
      var ps_chat_area = new PerfectScrollbar(".chat-area", {
         theme: "dark"
      });
   }

   // Close other sidenav on click of any sidenav
   $(".sidenav-trigger").on("click", function () {
      if ($(window).width() < 960) {
         $(".sidenav").sidenav("close");
         $(".app-sidebar").sidenav("close");
      }
   });

   // Toggle class of sidenav
   $("#chat-sidenav").sidenav({
      onOpenStart: function () {
         $("#sidebar-list").addClass("sidebar-show");
      },
      onCloseEnd: function () {
         $("#sidebar-list").removeClass("sidebar-show");
      }
   });

   // Favorite star click
   $(".favorite i").on("click", function () {
      $(this).toggleClass("amber-text");
   });

   // For chat sidebar on small screen
   if ($(window).width() < 900) {
      $(".app-chat .sidebar-left.sidebar-fixed").removeClass("animate fadeUp animation-fast");
      $(".app-chat .sidebar-left.sidebar-fixed .sidebar").removeClass("animate fadeUp");
   }

   $(".chat-area").scrollTop($(".chat-area > .chats").height());

});

// Add message to chat
function enter_chat(source) {
   var message = $(".message").val();
   var d = new Date();
   var date = d.getDate();
   var month = d.getMonth();
   var year = d.getFullYear();
   var hour = d.getHours();
   var minute = d.getMinutes();
   function pad(number) {
      return number < 10 ? '0' + number : number;
   }
   var dateTime = pad(date) + '/' + pad(month) + '/' + year + ' ' + pad(hour) + ':' + pad(minute);
   if (message != "") {
      var html = '<div class="chat-text">'
         + '<div class="date-time center-align">' + dateTime + '</div>'
         + '<div class="text-content">'
         + '<div class="content">'
         + '<p>' + message + '</p>'
         + '</div>'
         + '</div>'
         + '</div>'
      $(".chat:last-child .chat-body").append(html);
      $(".message").val("");
      $(".chat-area").scrollTop($(".chat-area > .chats").height());
   }
}

$(window).on("resize", function () {
   if ($(window).width() > 899) {
      $("#chat-sidenav").removeClass("sidenav");
   }

   if ($(window).width() < 900) {
      $("#chat-sidenav").addClass("sidenav");
   }
});
