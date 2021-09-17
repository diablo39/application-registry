import { PagingInfoData } from './PagingInfoData'

export class PagingInfo implements PagingInfoData {
    sortBy: string[] = [];
    sortDesc: string[] = [];
    page = 1;
    itemsPerPage = -1;

    /**
     *
     */
    constructor(pagingInfoData: PagingInfoData) {
        if (pagingInfoData.sortBy)
            this.sortBy = pagingInfoData.sortBy;

        if (pagingInfoData.sortDesc)
            this.sortDesc = pagingInfoData.sortDesc;

        if (pagingInfoData.page)
            this.page = pagingInfoData.page;

        if (pagingInfoData.itemsPerPage)
            this.itemsPerPage = pagingInfoData.itemsPerPage;
    }

    clone(): PagingInfo {
        const result = new PagingInfo(this);

        return result;
    }

    static Default: PagingInfo = new PagingInfo({} as PagingInfoData);
}