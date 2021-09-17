import * as form from "@/services/formHelpers/FormHelpers";
import {ValidationRules} from "@/validation/ValidationRules";

export default class {
    code = new form.RowItem({
        label: "Code",
        validationRules: [
            ValidationRules.required(),
            ValidationRules.maxLength(25),
        ],
    });

    name = new form.RowItem({
        label: "Name",
        validationRules: [ValidationRules.required()],
    });

    description = new form.RowItem({
        label: "Description",
        validationRules: [],
    });

    owner = new form.RowItem({
        label: "Owner",
        validationRules: [
            ValidationRules.required(),
            //ValidationRules.maxLength(25),
        ],
    });

    repositoryUrl = new form.RowItem({
        label: "Repository Url",
        validationRules: [
            //ValidationRules.required(),
            //ValidationRules.maxLength(25),
        ],
    });
    buildProcessUrls = new form.RowItem({
        label: "Build process urls",
        validationRules: [
            //ValidationRules.required(),
            //ValidationRules.maxLength(25),
        ],
    });

    projectId = new form.RowItem({
        label: "System",
        validationRules: [ValidationRules.required()],
        itemText: "name",
        itemValue: "id",
        serializeAs: "idProject"
    });

    framework = new form.RowItem({
        label: "Framework",
        validationRules: [
            //ValidationRules.required(),
            //ValidationRules.maxLength(25),
        ],
    });

    endpoints = {
        value: [] as any[]
    }

}