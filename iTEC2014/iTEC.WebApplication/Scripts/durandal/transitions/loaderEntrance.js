/**
 * Durandal 2.0.1 Copyright (c) 2012 Blue Spire Consulting, Inc. All Rights Reserved.
 * Available via the MIT license.
 * see: http://durandaljs.com or https://github.com/BlueSpire/Durandal for details.
 */
/**
 * The entrance transition module.
 * @module entrance
 * @requires system
 * @requires composition
 * @requires jquery
 */
define(['durandal/system', 'durandal/composition', 'jquery'], function(system, composition, $) {
    var duration = 100;

    /**
     * @class EntranceModule
     * @constructor
     */
    var entrance = function(context) {
        return system.defer(function (dfd) {
            function endTransition() {
                dfd.resolve();
            }

            function scrollIfNeeded() {
                if (!context.keepScrollPosition) {
                    $(document).scrollTop(0);
                }
            }

            if (!context.child) {
                $(context.activeView).fadeOut(duration, endTransition);
            } else {
                var fadeOnly = !!context.fadeOnly;

                function startTransition() {
                    scrollIfNeeded();
                    context.triggerAttach();

                    var $child = $(context.child);
                    $child.fadeIn({ duration: duration * 2, always: endTransition });
                }

                if (context.activeView) {
                    var interval = setInterval(function () {
                        if ($('body').hasClass('loaded')) {
                            setTimeout(function () {
                                $(context.activeView).fadeOut({ duration: duration * 2, always: startTransition });
                            }, 1000);
                            clearInterval(interval);
                        }
                    }, duration);
                } else {
                    startTransition();
                }
            }
        }).promise();
    };

    return entrance;
});
