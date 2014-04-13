define(['jquery', 'knockout'],
    function ($, ko) {

        ko.bindingHandlers.chart = {
            update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var $element = $(element);
                var options = ko.utils.unwrapObservable(valueAccessor());
                if (options && options.chart) {
                    $element.highcharts(options);
                }
            }
        };
        
        ko.bindingHandlers.slider = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var $element = $(element);
                var options = ko.toJS(valueAccessor().options);

                $element.slider(options).on('slide', function (e) {
                    valueAccessor().points(e.value);
                });
            }
        };

        ko.bindingHandlers.fade = {
            update: function (element, valueAccessor) {
                var ready = ko.utils.unwrapObservable(valueAccessor().value);
                var duration = valueAccessor().duration;
                var $element = $(element);
                if (ready) {
                    $element.fadeOut({ duration: duration, complete: function () { $element.addClass('hidden') } });
                } else {
                    $element.fadeIn({ duration: duration, complete: function () { $element.removeClass('hidden') } });
                }
            }
        };

        ko.bindingHandlers.hidden = {
            update: function (element, valueAccessor) {
                var value = ko.utils.unwrapObservable(valueAccessor());
                ko.bindingHandlers.visible.update(element, function () { return !value; });
            }
        };

        ko.bindingHandlers['validationTitleMessage'] = { // individual error message, if modified or post binding
            update: function (element, valueAccessor) {
                var obsv = valueAccessor(),
                    config = ko.validation.utils.getConfigOptions(element),
                    val = ko.utils.unwrapObservable(obsv),
                    msg = null,
                    isModified = false,
                    isValid = false;

                obsv.extend({ validatable: true });

                isModified = obsv.isModified();
                isValid = obsv.isValid();

                // create a handler to correctly return an error message
                var errorMsgAccessor = function () {
                    if (!config.messagesOnModified || isModified) {
                        return isValid ? null : obsv.error;
                    } else {
                        return null;
                    }
                };

                //toggle visibility on validation messages when validation hasn't been evaluated, or when the object isValid
                var visiblityAccessor = function () {
                    return isModified ? !isValid : false;
                };

                ko.bindingHandlers.attr.update(element, function () { return { title: errorMsgAccessor() } });
                ko.bindingHandlers.visible.update(element, visiblityAccessor);
            }
        };

        ko.bindingHandlers.validate = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var value = valueAccessor();
                var entity = ko.utils.unwrapObservable(value.entity());

                var submit = function () {
                    return function () {
                        var $form = $(element);
                        if (entity.validate()) {
                            var $element = $form.find('button') || $form.find('input[type=submit]');
                            var text = $element.text();
                            $element.attr('disabled', 'disabled').html($element.data('loading-text') || 'Loading...');

                            value.submit().always(function () {
                                $element.removeAttr('disabled').html(text);
                            });
                        }
                    };
                };

                ko.bindingHandlers['submit'].init(element, submit, allBindingsAccessor, viewModel, bindingContext);
            }
        };

        ko.bindingHandlers.checkbox = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var value = valueAccessor();

                if (typeof value !== 'function') {
                    return;
                }

                var $element = $(element);

                if ($element.hasClass('checkbox text-selection-none')) {
                    $element.addClass('checkbox text-selection-none');
                }
                $element.bind('click', toggle);

                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    $element.unbind('click', toggle);
                });

                function toggle() {
                    value(!value());
                }
            },
            update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var value = valueAccessor();

                var $element = $(element);
                var $icon = $('i', $element);
                if (value()) {
                    $icon.removeClass('fa-times');
                    $icon.addClass('fa-check');
                } else {
                    $icon.removeClass('fa-check');
                    $icon.addClass('fa-times');
                }
            }
        };

        ko.bindingHandlers.dateFormat = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                //moment(baseModel.actionDate()).format('LL')
                var observable = valueAccessor().value,
                    formatted = ko.computed({
                        read: function (key) {
                            return (observable())
                                ? moment(observable()).format(valueAccessor().format)
                                : 'unknown';
                        },
                        disposeWhenNodeIsRemoved: element
                    });

                //apply the actual value binding with our new computed
                ko.applyBindingsToNode(element, { text: formatted });
            }
        };

        ko.bindingHandlers.infinite = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var value = valueAccessor().value;
                var load = valueAccessor().load;
                var offset = ko.utils.unwrapObservable(valueAccessor().offset);

                var $element = $(element);

                var self = this;

                $element.bind('scroll', function (event) {
                    var elem = event.target;
                    if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - offset)) {
                        load();
                    }
                });
                load(true);
                
                var $result = $element.children('.result');
                ko.bindingHandlers.foreach.init($result[0], function () { return value }, allBindingsAccessor, viewModel, bindingContext);
            },
            update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var value = valueAccessor().value;
                var empty = ko.computed(function () {
                    return !(value() != undefined && value().length > 0);
                });

                var $element = $(element);
                var $result = $element.children('.result');
                ko.bindingHandlers.foreach.update($result[0], function () { return value }, allBindingsAccessor, viewModel, bindingContext);
                ko.bindingHandlers.css.update($result[0], function () { return { empty: empty } });

                var $status = $element.children('.status');
                ko.bindingHandlers.visible.update($status[0], function () { return value.infinite.isUpdating });

                var $message = $element.children('.message');
                ko.bindingHandlers.visible.update($message[0], function () { return !value.infinite.isUpdating() && empty() });
            }
        };

        ko.bindingHandlers.modal = {
            init: function (element, valueAccessor) {
                var value = valueAccessor();
                $(element).modal({ backdrop: true, keyboard: true, show: ko.utils.unwrapObservable(value) })
                    .on('hide.bs.modal', function (e) {
                        value(false);
                    });
            },
            update: function (element, valueAccessor) {
                var value = valueAccessor();
                if (ko.utils.unwrapObservable(value)) {
                    $(element).modal('show');
                }
                else {
                    $(element).modal('hide');
                }
            }
        };

        ko.observableArray.fn.pushAll = function (valuesToPush) {
            var underlyingArray = this();
            this.valueWillMutate();
            ko.utils.arrayPushAll(underlyingArray, valuesToPush);
            this.valueHasMutated();
            return this;  //optional
        };
    }
);