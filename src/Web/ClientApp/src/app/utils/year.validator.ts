import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function yearValidator(minYear: number, maxYear: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const currentYear = new Date().getFullYear();
    const value = control.value;

    if (value < minYear || value > currentYear) {
      return { 'invalidYear': { value: control.value } };
    }
    return null;
  };
}
