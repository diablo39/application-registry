export function serializeRow(row: object): any {
    const result = {};

    for (const item in row) {

        const rowItem = row[item] as RowItem;

        let value = row[item].value;

        if(rowItem.converter) {
            value = rowItem.converter(value);
        }
        result[rowItem.serializeAs || item] = value;
    }

    return result;
}

export function processErrors(errors, row) {
    for (let errorField in errors.response.data.errors) {
        const errorMessages = errors.response.data.errors[errorField];
        if (errorField && row[errorField]) {
            errorField =
                errorField[0].toLowerCase() +
                (errorField.length > 1 ? errorField.substring(1) : "");
            row[errorField].errorMessages = errorMessages;
        }
    }
}

export interface RowItemOptions {
    value?: unknown;
    label: string;
    validationRules: Array<Function>;
    itemText?: string;
    itemValue?: string;
    serializeAs?: string;
    readonly?: boolean;
    converter?: Function;
}

export class RowItem implements RowItemOptions {

    value: unknown;
    label: string;
    validationRules: Function[];
    itemText?: string | undefined;
    itemValue?: string | undefined;
    serializeAs?: string
    errorMessages: Array<string> = [];
    readonly?: boolean = false;
    converter?: Function;

    constructor(options: RowItemOptions) {
        this.value = options.value || "";
        this.label = options.label;
        this.validationRules = options.validationRules;
        this.itemText = options.itemText;
        this.itemValue = options.itemValue;
        this.serializeAs = options.serializeAs;
        this.readonly = options.readonly || false;
        this.converter = options.converter;
    }
}

export function BooleanConverter(item) {
    const isSet = (item !== '' && item !== null);

    return isSet ? item : false;
}