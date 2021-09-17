export function serializeRow(row: object): any {
    const result = {};
  
    for (const item in row) {

      const rowItem = row[item] as RowItem;
      result[rowItem.serializeAs || item] = row[item].value;
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
    value?: string;
    label: string;
    validationRules: Array<Function>;
    itemText?: string;
    itemValue?: string;
    serializeAs?: string;
    readonly?: boolean;
  }
  
export class RowItem implements RowItemOptions {
    
    value: string;
    label: string;
    validationRules: Function[];
    itemText?: string|undefined;
    itemValue?: string|undefined;
    serializeAs?: string
    errorMessages: Array<string> = [];
    readonly?: boolean = false;
  
    constructor(options: RowItemOptions) {
      this.value = options.value || "";
      this.label = options.label;
      this.validationRules = options.validationRules;
      this.itemText = options.itemText;
      this.itemValue = options.itemValue;
      this.serializeAs = options.serializeAs;
      this.readonly = options.readonly || false;
    }
  }