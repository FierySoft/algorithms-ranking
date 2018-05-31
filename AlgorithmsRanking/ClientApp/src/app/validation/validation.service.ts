export class ValidationService {
    static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
        const config = {
            'required': 'Введите значение',
            'pattern': 'Недопустимые символы',
            'minlength': `Минимальное количество ${validatorValue.requiredLength}`,
            'maxlength': `Максимальное количество ${validatorValue.requiredLength}`,
        };
        return config[validatorName];
    }
}
