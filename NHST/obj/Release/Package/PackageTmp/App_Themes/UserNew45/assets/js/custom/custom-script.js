/*================================================================================
	Item Name: Materialize - Material Design Admin Template
	Version: 5.0
	Author: PIXINVENT
	Author URL: https://themeforest.net/user/pixinvent/portfolio
================================================================================

NOTE:
------
PLACE HERE YOUR OWN JS CODES AND IF NEEDED.
WE WILL RELEASE FUTURE UPDATES SO IN ORDER TO NOT OVERWRITE YOUR CUSTOM SCRIPT IT'S BETTER LIKE THIS. */

$(document).ready(function() {
  //init modal trigger open
  $(".modal").modal();

  //Dropdown
  $(".table .dropdown-trigger").dropdown({
    inDuration: 350,
    outDuration: 225,
    constrainWidth: false, // Does not change width of dropdown to that of the activator
    hover: false, // Activate on hover
    gutter: 0, // Spacing from edge
    coverTrigger: false, // Displays dropdown below the button
    alignment: "left", // Displays dropdown with edge aligned to the left of button
    stopPropagation: false // Stops event propagation
  });

  //lightbox
  $(".materialboxed").materialbox();
  //filter toggle
  $("#filter-btn").on("click", function() {
    $(this).toggleClass("active");
    $(this)
      .parent()
      .parent()
      .find(".filter-wrap")
      .slideToggle();
  });
  //Datepicker
  //$(".from-date").each(function() {
  //  var $this = $(this);
  //  $this.datepicker({
  //    format: "dd-mm-yyyy",
  //    onSelect: function(selected) {
  //      var toDate = $this
  //        .parent()
  //        .next()
  //        .find(".to-date");
  //      toDate.datepicker({
  //        format: "dd-mm-yyyy",
  //        minDate: selected
  //      });
  //    }
  //  });
  //});

  //$(".to-date").each(function() {
  //  var $this = $(this);
  //  $this.on("click", function() {
  //    var fromDate = $this
  //      .parent()
  //      .prev()
  //      .find(".from-date")
  //      .val();
  //    if (fromDate == "") {
  //      $this.addClass("invalid");
  //      $(".to-date").datepicker("destroy");
  //    } else {
  //      $this.removeClass("invalid");
  //    }
  //  });
  //});

    $.datetimepicker.setLocale('vi');
    $('.datetimepicker').each(function () {
        $(this).attr('autocomplete', 'off');
        $(this).datetimepicker({
            format: 'd/m/Y H:00',
            formatDate: 'd/m/Y',
            allowTimes: [
                '00:00',
                '01:00',
                '02:00',
                '03:00',
                '04:00',
                '05:00',
                '06:00',
                '07:00',
                '08:00',
                '09:00',
                '10:00',
                '11:00',
                '12:00',
                '13:00',
                '14:00',
                '15:00',
                '16:00',
                '17:00',
                '18:00',
                '19:00',
                '20:00',
                '21:00',
                '22:00',
                '23:00',
            ],
            // theme:'dark',
            // defaultDate:new Date(),
            onShow: function (ct, element) {
                if ($(element).hasClass('to-date')) {
                    var minDate = $(element).closest('.row').find('.from-date').val();
                    console.log(minDate);
                    this.setOptions({
                        minDate: minDate ? minDate : false,
                    })
                }
                if ($(element).hasClass('date-only')) {
                    this.setOptions({
                        timepicker: false
                    })
                }
            }
        });
    })

  //Select price
  $(".from-price").on("change", function() {
    console.log($(this).val());
    $(".to-price").attr("min", $(this).val());
  });

  //select all dropdown
  $("select").formSelect();
  $("select.select_all")
    .siblings("ul")
    .prepend("<li id=sm_select_all><span>Chọn tất cả</span></li>");
  $("li#sm_select_all").on("click", function() {
    var jq_elem = $(this),
      jq_elem_span = jq_elem.find("span"),
      select_all = jq_elem_span.text() == "Chọn tất cả",
      set_text = select_all ? "Bỏ chọn tất cả" : "Chọn tất cả";
    jq_elem_span.text(set_text);
    jq_elem
      .siblings("li")
      .filter(function() {
        return (
          $(this)
            .find("input")
            .prop("checked") != select_all
        );
      })
      .click();
  });

  //Mask input type
  $('[data-type="phone-number"]').formatter({
    pattern: "{{99999999999}}"
  });
  $('[data-type="dateofbirth"]').formatter({
    pattern: "{{99}}-{{99}}-{{9999}}"
  });

  //click upload file
  $("body").on("click", "button.btn-upload", function() {
    $(this)
      .siblings('input[type="file"]')
      .trigger("click");
  });

  //up-downControl
  $(".up-downControl").each(function() {
    var $this = $(this);
    var step = parseInt($this.attr("data-step"));
    var min = parseInt($this.attr("data-min"));
    var max = parseInt($this.attr("data-max"));

    $this.find(".btn").on("click", function(e) {
      e.preventDefault();
      var value = parseInt($this.find(".value").val());
      if ($(this).hasClass("minus")) {
        value = value - step;
        if (value < min) return;
      }
      if ($(this).hasClass("plus")) {
        value = value + step;
        if (value > max) return;
      }
      console.log(value);
      $this.find(".value").val(value);
    });
  });
  // ===== Scroll to Top ====
  $(window).scroll(function() {
    if ($(this).scrollTop() >= 50) {
      // If page is scrolled more than 50px
      $("#return-to-top").fadeIn(200); // Fade in the arrow
    } else {
      $("#return-to-top").fadeOut(200); // Else fade out the arrow
    }
  });
  $("#return-to-top").click(function() {
    // When arrow is clicked
    $("body,html").animate(
      {
        scrollTop: 0 // Scroll to top of body
      },
      500
    );
    });

    $('.toggle-download').on('click', function () {
        $(this).closest('.fixed-download').toggleClass('open');
    });
});

window.addEventListener("DOMContentLoaded", function() {
  var elementScroll = document.querySelectorAll(".responsive-tb");

  if (elementScroll != undefined || elementScroll != null) {
    elementScroll.forEach(function(element) {
      var mx = 0;
      element.addEventListener("mousedown", function(e) {
        this.sx = this.scrollLeft;
        mx = e.pageX - this.offsetLeft;

        this.addEventListener("mousemove", mouseMoveFunction);
      });
      element.addEventListener("mouseup", function(e) {
        this.removeEventListener("mousemove", mouseMoveFunction);
        mx = 0;
      });
      function mouseMoveFunction(e) {
        var mx2 = e.pageX - this.offsetLeft;
        if (mx) this.scrollLeft = this.sx + mx - mx2;
      }
    });
  }

  //add attribute data-type="currency" for input to set input type currency format
  var inputMoney = document.querySelectorAll('[data-type="currency"]');
  function inputNumberTest() {
    var regx = /\D+/g;
    var number = this.value.replace(regx, "");

    return (this.value = number.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
  }
  inputMoney.forEach(function(input, index) {
    input.addEventListener("input", inputNumberTest);
  });
});

//Upload image preview
function previewFiles(e) {
  var preview = e.parentNode.nextElementSibling;
  var files = e.files;
  function readAndPreview(file) {
    // Make sure `file.name` matches our extensions criteria
    if (/\.(jpe?g|png|gif)$/i.test(file.name)) {
      var reader = new FileReader();

      reader.addEventListener(
        "load",
        function() {
          var image = new Image();
          image.height = 50;
          image.title = file.name;
          image.src = this.result;
          image.classList = "materialboxed";
          var wrap = document.createElement("div");
          wrap.classList = "img-block";
          wrap.appendChild(image);
          var close = document.createElement("span");
          close.classList = "material-icons red-text delete";
          close.innerHTML = "clear";
          close.onclick = function() {
            this.parentNode.parentNode.removeChild(this.parentNode);
          };
          wrap.appendChild(close);
          preview.appendChild(wrap);
        },
        false
      );

      reader.readAsDataURL(file);
    }
  }

  if (files) {
    var promise = new Promise(function(resolve, reject) {
      resolve([].forEach.call(files, readAndPreview));
    });
    promise.then(function() {
      setTimeout(function() {
        $(".materialboxed").materialbox();
      }, 1000);
    });
  }
}
