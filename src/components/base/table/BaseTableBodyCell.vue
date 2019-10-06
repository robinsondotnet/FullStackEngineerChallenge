<template>
  <td :class="[$options.name, actionClass]">
    <span v-if="type === 'star'">
      <star-rating
        :class="`${$options.name}__star`"
        :star-size="20"
        :rating="value"
        :read-only="!editMode"
        @rating-selected="onRatingSelected"
      />
    </span>

    <base-multi-select
      v-else-if="type === 'multi-select'"
      :key="$vnode.key"
      v-model="value"
      :edit-mode="editMode"
      :component="customComponent"
    />

    <span v-else>
      <base-text-box
        :edit-mode="editMode"
        :type="type"
        v-model="value"
      ></base-text-box>
    </span>
  </td>
</template>

<script>
import BaseTextBox from "@/components/base/input/BaseTextBox.vue";
import StarRating from "vue-star-rating";
import BaseMultiSelect from "../input/BaseMultiSelect";

export default {
  name: "BaseTableBodyCell",

  model: {
    prop: "model",
    event: "change"
  },

  components: {
    BaseMultiSelect,
    BaseTextBox,
    StarRating
  },

  props: {
    model: [String, Number, Array],
    type: {
      type: String,
      default() {
        return "text";
      }
    },
    editMode: {
      type: Boolean,
      default() {
        return false;
      }
    },
    customComponent: String
  },

  watch: {
    value(val) {
      this.$emit("change", val);
    },
    model(val) {
      this.value = val;
    }
  },

  data() {
    return {
      value: null
    };
  },

  computed: {
    isAction() {
      return this.type === "action";
    },

    actionClass() {
      return this.isAction ? `${this.$options.name}--action` : "";
    }
  },

  methods: {
    onRatingSelected(evt) {
      this.$emit("change", evt);
    }
  },

  mounted() {
    this.value = this.model;
  }
};
</script>

<style lang="scss">
.BaseTableBodyCell {
  padding: 0px;
  border-right: 0.05px solid gray;
  width: 200px;

  &--action {
    width: 50px;
  }

  &__star {
    justify-content: center;
  }
}
</style>
