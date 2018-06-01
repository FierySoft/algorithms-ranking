import { Directive, forwardRef } from '@angular/core';
import { AbstractControl, Validator, ValidationErrors, NG_VALIDATORS } from '@angular/forms';

@Directive({
    selector: '[validatePersonName]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => PersonNameValidatorDirective), multi: true }
    ]
})
export class PersonNameValidatorDirective implements Validator {
    pattern = '^[А-ЯЁа-яё][А-ЯЁа-яё]*([ |-][А-ЯЁа-яё])?[А-ЯЁа-яё]*$';
    regexp = new RegExp(this.pattern);

    validate(c: AbstractControl): ValidationErrors | null {
        if (c.value && !this.regexp.test(c.value)) {
            return {
                pattern: false,
                message: 'Недопустимые символы.'
            };
        }
    }
}
