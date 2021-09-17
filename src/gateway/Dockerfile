FROM envoyproxy/envoy:v1.17.0
ARG  CONFIGFILE=envoy.yaml
RUN echo ${CONFIGFILE}
COPY ${CONFIGFILE} /etc/envoy/envoy.yaml
RUN chmod go+r /etc/envoy/envoy.yaml