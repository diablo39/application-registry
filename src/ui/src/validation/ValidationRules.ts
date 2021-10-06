export class ValidationRules {
    
    static required() {
        return (v: string) => !!v || `Field is required`;
    }

    static maxLength(maxLength: number) {
        return (v: string) => (v || '').length <= maxLength || `A maximum of ${maxLength} characters is allowed`
    }

    static lessThen(maxValue: number) {
        return (v: any) => parseInt((v || 0).toString()) < maxValue || `Value must be lower then ${maxValue}`
    }
}