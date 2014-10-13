(function($) {
  $.fn.class = function(condition, classes) {
    return condition ? this.addClass(classes)
      : this.removeClass(classes);
  }
}(jQuery))