(function(e){function t(t,n){n=n||{};var r=this,i=n.query||"> :even";e.extend(r,{$el:t,options:n,sections:[],isAccordion:n.accordion||false,db:n.persist?jQueryCollapseStorage(t.get(0).id):false});r.states=r.db?r.db.read():[];r.$el.find(i).each(function(){new jQueryCollapseSection(e(this),r)});(function(t){r.$el.on("click","[data-collapse-summary] "+(t.options.clickQuery||""),e.proxy(r.handleClick,t));r.$el.bind("toggle close open",e.proxy(r.handleEvent,t))})(r)}function n(t,n){if(!n.options.clickQuery)t.wrapInner("<a/>");e.extend(this,{isOpen:false,$summary:t.attr("data-collapse-summary",""),$details:t.next(),options:n.options,parent:n});n.sections.push(this);var r=n.states[this._index()];if(r===0){this.close(true)}else if(this.$summary.is(".open")||r===1){this.open(true)}else{this.close(true)}}function i(t){var n;try{n=window.localStorage||e.fn.collapse.cookieStorage}catch(r){n=false}return n?new s(t,n):false}function s(e,t){this.id=e;this.db=t;this.data=[]}t.prototype={handleClick:function(t,n){t.preventDefault();var n=n||"toggle";var r=this.sections,i=r.length;while(i--){if(e.contains(r[i].$summary[0],t.target)){r[i][n]();break}}},handleEvent:function(e){if(e.target==this.$el.get(0))return this[e.type]();this.handleClick(e,e.type)},open:function(t){if(isFinite(t))return this.sections[t].open();e.each(this.sections,function(e,t){t.open()})},close:function(t){if(isFinite(t))return this.sections[t].close();e.each(this.sections,function(e,t){t.close()})},toggle:function(t){if(isFinite(t))return this.sections[t].toggle();e.each(this.sections,function(e,t){t.toggle()})}};n.prototype={toggle:function(){this.isOpen?this.close():this.open()},close:function(e){this._changeState("close",e)},open:function(t){var n=this;if(n.options.accordion&&!t){e.each(n.parent.sections,function(e,t){t.close()})}n._changeState("open",t)},_index:function(){return e.inArray(this,this.parent.sections)},_changeState:function(t,n){var r=this;r.isOpen=t=="open";if(e.isFunction(r.options[t])&&!n){r.options[t].apply(r.$details)}else{r.$details[r.isOpen?"show":"hide"]()}r.$summary.toggleClass("open",t!="close");r.$details.attr("aria-hidden",t=="close");r.$summary.attr("aria-expanded",t=="open");r.$summary.trigger(t=="open"?"opened":"closed",r);if(r.parent.db){r.parent.db.write(r._index(),r.isOpen)}}};e.fn.extend({collapse:function(n,r){var i=r?e("body").find("[data-collapse]"):e(this);return i.each(function(){var i=r?{}:n,s=e(this).attr("data-collapse")||"";e.each(s.split(" "),function(e,t){if(t)i[t]=true});new t(e(this),i)})}});e(function(){e.fn.collapse(false,true)});jQueryCollapse=t;jQueryCollapseSection=n;var r="jQuery-Collapse";s.prototype={write:function(t,n){var i=this;i.data[t]=n?1:0;e.each(i.data,function(e){if(typeof i.data[e]=="undefined"){i.data[e]=0}});var s=this._getDataObject();s[this.id]=this.data;this.db.setItem(r,JSON.stringify(s))},read:function(){var e=this._getDataObject();return e[this.id]||[]},_getDataObject:function(){var e=this.db.getItem(r);return e?JSON.parse(e):{}}};jQueryCollapseStorage=i})(window.jQuery)