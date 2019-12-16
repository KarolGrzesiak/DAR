<template>
  <v-container v-if="loading" fluid fill-height>
    <v-row justify="center" align="center">
      <v-progress-circular
        indeterminate
        :size="150"
        :width="8"
        color="teal"
      ></v-progress-circular>
    </v-row>
  </v-container>
  <v-container v-else fluid fill-height>
    <v-row style="height: 100%;" class="ma-10" align="center" justify="center">
      <v-col cols="12">
        <v-row align="center" justify="center" no-gutters>
          <v-expansion-panels>
            <v-expansion-panel
              v-for="(id, i) in this.ids"
              :key="i"
              ref="expansionPanel"
            >
              <v-expansion-panel-header>{{ id }}</v-expansion-panel-header>
              <v-expansion-panel-content>
                <v-row justify="center">
                  <v-btn color="teal" class="white--text mr-2" style="width:45%"
                    >Deploy</v-btn
                  >
                  <v-btn
                    color="error"
                    class="white--text"
                    style="width:45%"
                    @click="dialog = true"
                    >Delete</v-btn
                  >
                  <v-dialog v-model="dialog" max-width="290">
                    <v-card>
                      <v-card-title class="headline"
                        >Are you sure?</v-card-title
                      >

                      <v-card-text
                        >Proceeding will permanently delete file.</v-card-text
                      >

                      <v-card-actions>
                        <v-spacer></v-spacer>

                        <v-btn
                          color="green darken-1"
                          text
                          @click="dialog = false"
                          >No</v-btn
                        >

                        <v-btn
                          color="green darken-1"
                          text
                          @click="remove(id, i)"
                          >Yes</v-btn
                        >
                      </v-card-actions>
                    </v-card>
                  </v-dialog>
                </v-row>
              </v-expansion-panel-content>
            </v-expansion-panel>
          </v-expansion-panels>
        </v-row>
        <v-row no-gutters align="center" justify="center">
          <v-file-input
            @change="uploadFile"
            ref="fileInput"
            v-model="files"
            prepend-icon="mdi-xml"
            multiple
            label="Upload HML file"
            filled
          ></v-file-input>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import ApiService from "../services/ApiService";
export default {
  data() {
    return {
      ids: [],
      loading: true,
      dialog: false,
      files: []
    };
  },
  mounted() {
    ApiService.get("/api/hml").then(response => {
      this.ids = response.data.map(function(hml) {
        return hml.id;
      });
    });
    this.loading = false;
  },
  methods: {
    remove(id, position) {
      this.dialog = false;
      ApiService.delete("/api/hml/" + id);
      this.ids.splice(position, 1);
      this.$refs.expansionPanel[position].isActive = false;
    },
    uploadFile() {
      if (this.files.length > 0) {
        let formData = new FormData();
        for (var i = 0; i < this.files.length; i++) {
          let file = this.files[i];
          formData.append("files[" + i + "]", file);
        }

        ApiService.post("/api/hml", formData)
          .then(() => {
            this.files.forEach(x => {
              this.ids.push(x.name);
            });
          })
          .catch(() => {})
          .finally(() => {
            this.files = [];
            this.$refs.fileInput.isFocused = false;
          });
      }
    }
  }
};
</script>
