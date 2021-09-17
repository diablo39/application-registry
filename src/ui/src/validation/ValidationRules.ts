export class ValidationRules {
    
    static required() {
        return (v: string) => !!v || `Field is required`;
    }

    static maxLength(maxLength: number) {
        return (v: string) => (v || '').length <= maxLength || `A maximum of ${maxLength} characters is allowed`
    }
}