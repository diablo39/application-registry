<script lang="ts">
import Vue from "vue";
import {
  HttpClient,
  PagingInfoData,
  PagingInfo,
} from "@/services/httpClient/HttpClient";
import global from "@/global";

export default Vue.component('v-ajax-list-data-source', {
  props: {
    httpPath: {
      type: String,
      required: true,
    },
    useServerSidePagination: {
      type: Boolean,
      default: global.config.useServerSidePagination
    },
    options: {
      default: function () {
        return {};
      }
    },
  },
  data() {
    return {
      filters: {},

      isLoading: true,
      isError: false,
      items: new Array<object>(),
      totalItems: -1,
    };
  },
  watch: {
    filters: {
      handler() {
        if (this.useServerSidePagination)
          console.log("filters changed in server mode");
      },
      deep: true,
    },
    options: {
      handler() {
        if (this.useServerSidePagination) {
          console.log("loading data");
          this.loadData();
        }
      },
      deep: true,
    },
  },
  mounted() {
    // when no server mode then get data from server after component is mounted
    if (!this.useServerSidePagination)
      this.loadData();
  },
  computed: {

    filteredItems(): Array<object> {

      if (Object.keys(this.filters).length == 0 || this.useServerSidePagination) return this.items; // initial state. No filters bound

      const localFilters = {};

      for (const filterField in this.filters) {
        const searchFor = this.filters[filterField];
        if (!searchFor) continue;

        localFilters[filterField] = this.filters[filterField].toLowerCase();
      }

      const result = new Array<object>();

      for (const item of this.items) {
        let isValid = true;

        for (const filterField in localFilters) {
          const searchFor = localFilters[filterField];
          const value: string = (item[filterField] || "").toString().toLowerCase();

          if (searchFor && value.indexOf(searchFor) === -1) {
            isValid = false;
            break;
          }
        }

        if (isValid) {
          result.push(item);
        }
      }

      return result;
    },
  },
  methods: {
    loadData() {
      this.isLoading = true;

      HttpClient.get(
          this.httpPath,
          new PagingInfo(this.options as PagingInfoData),
          this.useServerSidePagination,
      )
          .then((response) => {
            if (this.useServerSidePagination) {
              this.totalItems = response.data.totalCount;
            }
            this.items = response.data.items;
          })
          .catch(() => {
            this.isError = true;
          })
          .finally(() => {
            this.isLoading = false;
          });
    },
  },
  render(): any {
    return (this.$scopedSlots as any).default({
      ds: {
        filters: this.filters,
        options: this.options,
        isLoading: this.isLoading,
        isError: this.isError,
        filteredItems: this.filteredItems,
        totalItems: this.totalItems,
      },
      options: this.options,
    })
  }
});
</script>