<script lang="ts">
import Vue from "vue";


export default Vue.component('v-in-memory-list-data-source',{
  props: {
    items: {
      required: true,
      type: Array,
      default() { return []; },
    },
    isLoading: {},
    isError: {},
  },
  data() {
    return {
      filters: {},
      options: {itemsPerPage: 15},
      totalItems: -1,
    };
  },
  watch: {
    // filters: {
    //   handler() {
    //     //
    //   },
    //   deep: true,
    // },
    options: {
      handler() {
        // in server mode we should get data portion from server
      },
      deep: true,
    },
  },
  mounted() {
    //
  },
  computed: {

    filteredItems(): Array<object> {

      if (Object.keys(this.filters).length == 0) return this.items as Array<object>; // initial state. No filters bound

      const localFilters = {};

      for (const filterField in this.filters) {
        const searchFor = this.filters[filterField];
        if (!searchFor) continue;

        localFilters[filterField] = this.filters[filterField].toLowerCase();
      }

      const result = new Array<object>();

      for (const item of this.items) {
        const itemObject = item as object;

        let isValid = true;

        for (const filterField in localFilters) {
          const searchFor = localFilters[filterField];
          const value: string = (itemObject[filterField] || "").toString().toLowerCase();

          if (searchFor && value.indexOf(searchFor) === -1) {
            isValid = false;
            break;
          }
        }

        if (isValid) {
          result.push(itemObject);
        }
      }

      return result;
    },
  },
  methods: {

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
    })
  }
});
</script>